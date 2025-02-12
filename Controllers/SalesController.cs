

using Microsoft.AspNetCore.Mvc;

public class SalesController : Controller
{
    public IActionResult Customers()
    {
       ViewBag.Customers = new List<dynamic>
        {
            new { Id = 1, Name = "John Doe", Email = "john@example.com", Phone = "123-456-7890", JoinedDate = "2024-05-01" },
            new { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Phone = "987-654-3210", JoinedDate = "2024-06-15" }
        };

        return PartialView("Customers");
    }
    public IActionResult Orders()
    {
         ViewBag.Orders = new List<dynamic>
        {
            new { Id = 101, CustomerName = "John Doe", Amount = "$150", Status = "Completed", Date = "2025-02-12" },
            new { Id = 102, CustomerName = "Jane Smith", Amount = "$200", Status = "Pending", Date = "2025-02-11" }
        };

        return PartialView("Orders");
    }
}