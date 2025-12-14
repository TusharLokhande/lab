using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Email.Models
{
    public class EmailSettings
    {
        public string Host { get; set; } = default!;
        public int Port { get; set; }
        public string FromAddress { get; set; } = default!;
        public string FromName { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public bool EnableSsl { get; set; }
    }
}
