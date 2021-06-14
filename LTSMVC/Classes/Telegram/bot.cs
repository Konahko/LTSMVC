using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using LTSMVC.Classes.Telegram.commands;
using LtsContext = LTSMVC.Models;

namespace LTSMVC.Classes.Telegram
{
    public class Bot
    {
        private static TelegramBotClient client;
        private static List<Command> commandsList;

        public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();

        public static async Task<TelegramBotClient> GetBotClientAsync()
        {
            if (client != null)
            {
                return client;
            }

            commandsList = new List<Command>();
            commandsList.Add(new StartCommand());
            commandsList.Add(new RegisterCommand());
            //TODO: Add more commands

            client = new TelegramBotClient(AppSettings.Key);
            string hook = string.Format(AppSettings.Url, "api/telegram/update");
            await client.SetWebhookAsync(hook);
            return client;
        }
    }
}
