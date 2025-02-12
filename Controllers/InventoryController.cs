using Microsoft.AspNetCore.Mvc;

public class InventoryController : Controller
{
    public IActionResult Stock()
    {
        return PartialView("_Stock");
    }

    public IActionResult Suppliers()
    {
        return PartialView("_Suppliers");
    }
}
