using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace ERPSYS.Controllers
{

    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            // ✅ Get JWT Token from Cookie
            if (!Request.Cookies.TryGetValue("jwt", out var token) || string.IsNullOrEmpty(token))
            {
                return RedirectToLogin();
            }

            // ✅ Check if Token is Expired
            if (IsTokenExpired(token))
            {
                Response.Cookies.Delete("jwt"); // Remove expired token
                return RedirectToLogin(); // Redirect to login
            }

            // ✅ Create HttpClient instance
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //for production
            // var response = await client.GetAsync("https://erpsys-29yn.onrender.com/Dashboard");
            //for local testing
            var response = await client.GetAsync("http://localhost:8080/Dashboard");

            // ✅ If API returns 401, redirect to login
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Response.Cookies.Delete("jwt"); // Remove invalid token
                return RedirectToLogin(); // Redirect to login
            }

            var data = await response.Content.ReadAsStringAsync();
            ViewBag.Data = data;
            ViewBag.Token = token;
            ViewBag.SalesData = new List<int> { 12000, 15000, 11000, 18000, 17000, 20000 };
            ViewBag.Labels = new List<string> { "January", "February", "March", "April", "May", "June" };
            ViewBag.RevenueDistribution = new List<int> { 40, 30, 30 };
            ViewBag.RevenueLabels = new List<string> { "Product A", "Product B", "Product C" };

            return View();
        }

        // ✅ Check if JWT Token is Expired
        private bool IsTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            return jwtToken.ValidTo < DateTime.UtcNow; // Token expired if expiration date is in the past
        }

        // ✅ Redirect to Login
        private IActionResult RedirectToLogin()
        {
            return RedirectToAction("Index", "Login");
        }
        // Example Controller Code
        public async Task<IActionResult> Mains()
        {
            ViewBag.Username = "Khea On";  // Simulated user
            ViewBag.Notifications = new string[] { "New Order Received", "Task Deadline Approaching", "New Employee Added" };
            ViewBag.RecentActivities = new string[]
            {
                "Updated Inventory",
                "Completed Task: Review Reports",
                "New User Registered"
            };
            // ✅ Get JWT Token from Cookie
            if (!Request.Cookies.TryGetValue("jwt", out var token) || string.IsNullOrEmpty(token))
            {
                return RedirectToLogin();
            }

            // ✅ Check if Token is Expired
            if (IsTokenExpired(token))
            {
                Response.Cookies.Delete("jwt"); // Remove expired token
                return RedirectToLogin(); // Redirect to login
            }

            // ✅ Create HttpClient instance
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //var response = await client.GetAsync("https://erpsys-29yn.onrender.com/Dashboard");
            // for local testing
            var response = await client.GetAsync("http://localhost:8080/Dashboard");
            // ✅ If API returns 401, redirect to login
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Response.Cookies.Delete("jwt"); // Remove invalid token
                return RedirectToLogin(); // Redirect to login
            }

            var data = await response.Content.ReadAsStringAsync();
            ViewBag.Data = data;
            ViewBag.Token = token;

            return View();
        }
        public IActionResult Dashboard()
        {
            ViewBag.SalesData = new List<int> { 12000, 15000, 11000, 18000, 17000, 20000 };
            ViewBag.Labels = new List<string> { "January", "February", "March", "April", "May", "June" };
            ViewBag.RevenueDistribution = new List<int> { 40, 30, 30 };
            ViewBag.RevenueLabels = new List<string> { "Product A", "Product B", "Product C" };

            return PartialView("Dashboard");
        }

    }
}