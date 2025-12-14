using ExpenseTracker.Application.Common;
using ExpenseTracker.Application.Interfaces.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Email.Providers
{
    public class SendGridEmailService : IEmailService
    {
        public Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
