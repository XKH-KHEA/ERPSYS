﻿using ERPSYS.Data;
using ERPSYS.Models;
using ERPSYS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ERPSYS.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtTokenService _jwtTokenService;

        public LoginController(ApplicationDbContext context, JwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Username and password are required.";
                return View("Index");
            }

            // Find the user (case-insensitive search)
            var user = await _context.Users
                .Where(u => u.Username.ToLower() == username.ToLower())
                .FirstOrDefaultAsync();

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View("Index");
            }

            // Check password hash
            if (user.PasswordHash != HashPassword(password))
            {
                ViewBag.Error = "Invalid username or password.";
                return View("Index");
            }

            // Generate JWT token
            var token = _jwtTokenService.GenerateToken(user.Username);

            // ✅ Store JWT token in HTTP-only cookie for future requests
            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddMinutes(1)
            });
            //// Store token in TempData for redirect
            //TempData["Token"] = token;

            //// Redirect to the main page after login
            return RedirectToAction("Mains", "Dashboard");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

    }

}
