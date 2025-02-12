//using System.Net.Http;
//using System.Net.Http.Headers;
//using Microsoft.AspNetCore.Mvc;

//public class HomeController : Controller
//{
//    private readonly IHttpClientFactory _httpClientFactory;

//    public HomeController(IHttpClientFactory httpClientFactory)
//    {
//        _httpClientFactory = httpClientFactory;
//    }

//    public async Task<IActionResult> Main()
//    {
//        // ✅ Get JWT Token from Cookie
//        if (!Request.Cookies.TryGetValue("jwt", out var token) || string.IsNullOrEmpty(token))
//        {
//            return RedirectToAction("Index", "Login");
//        }

//        // ✅ Create HttpClient instance
//        var client = _httpClientFactory.CreateClient();
//        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

//        var response = await client.GetAsync("http://localhost:5065/Home/Main");

//        if (!response.IsSuccessStatusCode)
//        {
//            return Unauthorized();
//        }

//        var data = await response.Content.ReadAsStringAsync();
//        ViewBag.Data = data;
//        ViewBag.Token = token;

//        return View();
//    }
//}
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Main()
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

        var response = await client.GetAsync("http://localhost:5065/Home/Main");

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
}
