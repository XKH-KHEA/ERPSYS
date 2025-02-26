using ERPSYS.Data;
using ERPSYS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ERPSYS.Controllers.FinanceControll
{
    public class ChartOfAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChartOfAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Display list of accounts
        public async Task<IActionResult> Index()
        {
            var accounts = await _context.ChartOfAccounts.ToListAsync();
            return PartialView("~/Views/Finance/GeneralLeger/ChartOfAccounts/Index.cshtml", accounts);
        }

        // ✅ Show form to create new account
        public IActionResult Create()
        {
            return PartialView("~/Views/Finance/GeneralLeger/ChartOfAccounts/Create.cshtml");
        }

        // ✅ Save new account (Fixed for AJAX)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChartOfAccount account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();

                // ✅ Return JSON response with redirect URL
                return RedirectToAction("Mains", "Dashboard");
            }

            // ✅ If validation fails, return the partial view
            return RedirectToAction("Mains", "Dashboard");
        }
    }
}
