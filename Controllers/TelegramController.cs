using Microsoft.AspNetCore.Mvc;
using ERPSYS.Services;
using System.Threading.Tasks;

namespace ERPSYS.Controllers
{
    public class TelegramController : Controller
    {
        private readonly TelegramBotService _telegramBotService;

        public TelegramController(TelegramBotService telegramBotService)
        {
            _telegramBotService = telegramBotService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return PartialView("Index");
        }

        // POST: Home/SendMessageToBot
        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {
            long chatId = 878128141; // Replace with the user's or group's chat ID

            // Send the message to the Telegram bot
            await _telegramBotService.SendMessageAsync(message, chatId);

            // Optionally, show a confirmation message to the user
            TempData["Message"] = "Your message has been sent to the bot!";
            return RedirectToAction("Mains", "Dashboard");
        }
   
    }

}
