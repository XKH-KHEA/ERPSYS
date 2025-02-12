using Microsoft.AspNetCore.Mvc;

public class HRController : Controller
{
    public IActionResult Payroll()
    {
        return PartialView("Payroll");
    }
}
