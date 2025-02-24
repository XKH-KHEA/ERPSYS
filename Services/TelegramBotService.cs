using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ERPSYS.Services
{
    public class TelegramBotService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly string _botToken = "7524187609:AAHyAtNcKexXI0kJLZsoodJke-oEo4urJsY"; // Replace with your bot token

        public TelegramBotService()
        {
            _botClient = new TelegramBotClient(_botToken);
        }

        // Method to send a message to a specific chat (user or group)
        public async Task SendMessageAsync(string message, long chatId)
        {
            await _botClient.SendMessage(chatId, message);
        }
    }
}
