﻿using ERPSYS.Data;
using ERPSYS.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static Dropbox.Api.TeamLog.EventCategory;

namespace ERPSYS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Get the connection string from environment variable or appsettings.json
            //var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");

            //if (string.IsNullOrEmpty(connectionString))
            //{
            //    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            //}
            //else if (connectionString.StartsWith("postgres://"))
            //{
            //    // Convert Heroku-style connection string to standard PostgreSQL format
            //    var uri = new Uri(connectionString);
            //    var userInfo = uri.UserInfo.Split(':');
            //    connectionString = $"Host={uri.Host};Port={uri.Port};Username={userInfo[0]};Password={userInfo[1]};Database={uri.AbsolutePath.TrimStart('/')};SSL Mode=Require;Trust Server Certificate=true;";
            //}

            // Configure PostgreSQL
            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseNpgsql(connectionString));
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configure JWT Authentication
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException(nameof(secretKey), "SecretKey cannot be null or empty.");
            }

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });


            builder.Services.AddAuthorization();
            builder.Services.AddSingleton<JwtTokenService>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<TelegramBotService>();
            builder.Services.AddSingleton<DropboxService>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            // Ensure the app runs on the correct port inside a container
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            //builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
            builder.WebHost.UseUrls($"http://localhost:{port}");

            var app = builder.Build();

            // Correct middleware order
            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection(); // Keep HTTPS in production
            }


            app.UseCors("AllowAll");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
