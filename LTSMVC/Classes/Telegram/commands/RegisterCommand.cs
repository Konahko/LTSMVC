using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using LtsContext = LTSMVC.Models;

namespace LTSMVC.Classes.Telegram.commands
{

    public class RegisterCommand : Command
    {
        public override string Name => "/Register";

        public override async Task Execute(Message message, TelegramBotClient botClient, LtsContext.Lts2Context _context)
        {
            var user = _context.Staff
                .Where(s => s.TgId == message.Chat.Id)
                .FirstOrDefault();
            if (user == null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id,
                    "Сообщите следующие цифры Вашему администратору " + message.Chat.Id,
                    parseMode: ParseMode.Markdown);
            }
            else
            {
                await botClient.SendTextMessageAsync(message.Chat.Id,
                "Вы уже зарегистрированы",
                parseMode: ParseMode.Markdown);
            }
        }
    }
}
