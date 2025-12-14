using ExpenseTracker.Application.Interfaces.External;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Email.Templates
{
    internal sealed class EmailTemplateProvider : IEmailTemplateProvider
    {
        private readonly string _folderPath;

        public EmailTemplateProvider(
            IHostEnvironment env
           )
        {
            _folderPath = Path.Combine(env.ContentRootPath, "EmailTemplates");
        }

        public async Task<string> GetTemplateAsync(string templateName)
        {
            if (string.IsNullOrWhiteSpace(templateName))
                throw new ArgumentException("Template name is required.", nameof(templateName));

            string filePath = Path.Combine(_folderPath, $"{templateName}.html");

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Email template '{templateName}' not found.", filePath);

            string content = await File.ReadAllTextAsync(filePath);

            return content;
        }
    }
}
