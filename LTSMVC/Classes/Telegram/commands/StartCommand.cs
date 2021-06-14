using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using LtsContext = LTSMVC.Models;
using Microsoft.EntityFrameworkCore;


namespace LTSMVC.Classes.Telegram.commands
{

    public class StartCommand : Command
    {
        public override string Name => "/start";

        public override async Task Execute(Message message, TelegramBotClient botClient, LtsContext.Lts2Context _context)
        {
            var user = _context.Staff
                .Where(s => s.TgId == message.Chat.Id)
                .FirstOrDefault();
            if (user == null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id,
                    "Добро пожаловать в бот программу поддержки локомотивного депо Юдино-Казанский,\n Ваша учетаная запись Telegram не загрезистрирована в системе \nНапишите /register для старта" +
                    "процедуры регистрации",
                    parseMode: ParseMode.Markdown);
            }
            else
            {
                await botClient.SendTextMessageAsync(message.Chat.Id,
                "С возвращением, " + user.StaffName,
                parseMode: ParseMode.Markdown);
            }
            
        }
    }
}
