
using Microsoft.AspNetCore.Mvc;

public class FinanceController : Controller
{
    public IActionResult Invoices()
    {
        return PartialView("Invoices");
    }
    public IActionResult Expenses()
    {
        return PartialView("Expenses");
    }
    public IActionResult Emloyeelist()
    {
        return PartialView("Emloyeelist");
    }
}
