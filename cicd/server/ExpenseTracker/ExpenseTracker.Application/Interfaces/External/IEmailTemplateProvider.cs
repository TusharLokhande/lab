using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Interfaces.External
{
    public interface IEmailTemplateProvider
    {
        Task<string> GetTemplateAsync(string templateName);
    }
}
