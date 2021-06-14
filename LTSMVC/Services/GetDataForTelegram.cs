using System;
using System.Collections.Generic;
using System.Linq;
using Thread = System.Threading.Tasks;
using LTSMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace LTSMVC.Services
{
    public class GetDataForTelegram
    {
        private readonly Lts2Context _context;
        public GetDataForTelegram(Lts2Context context)
        {
            _context = context;
        }

        public long GetUsegTgId(long fromUser)
        {
            var a = _context;
           long? userId = _context.Staff
                .Where(s => s.TgId == 0)
                .Select(s => s.TgId)
                .FirstOrDefault();
            if (userId != null)
                return (long)userId;
            else return 0;
        }
    }
}
