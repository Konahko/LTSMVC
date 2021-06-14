using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using LtsModels = LTSMVC.Models;

namespace LTSMVC.Classes.Telegram.commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract Task Execute(Message message, TelegramBotClient client, LtsModels.Lts2Context _context);

        public bool Contains(string command)
        {
            return command.Contains(this.Name) && command.Contains(AppSettings.Name);
        }
    }
}
