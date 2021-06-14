using LTSMVC.Classes.Telegram;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.IO;
using System.Text;
using System.Net.Http;
using LTSMVC.Models;

namespace LTSMVC.Controllers.Telegram
{
    [AllowAnonymous]
    [Route("api/telegram/update")]
    [ApiController]
    public class TelegramApiController : ControllerBase
    {

        private readonly Lts2Context _context;

        public TelegramApiController(Lts2Context context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            return ("s");
        }
        [HttpPost]
        public async Task<OkResult> Update([FromBody]Update update)
        {
                if (update == null) return Ok();

            var commands = Bot.Commands;
            var message = update.Message;
            var client = await Bot.GetBotClientAsync();

            foreach (var command in commands)
            {
                if (message.Text.StartsWith(command.Name, StringComparison.OrdinalIgnoreCase))
                {
                    await command.Execute(message, client, _context);
                    break;
                }
            }
            
            return Ok();
        }

    }

}

