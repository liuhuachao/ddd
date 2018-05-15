using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using WebApiAuth.Interfaces;

namespace WebApiAuth.Services
{
    public class CloundMailService : IMailService
    {
        private string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
        private string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];
        private readonly ILogger<CloundMailService> _logger;

        public CloundMailService(ILogger<CloundMailService> logger)
        {
            this._logger = logger;
        }

        public void Send(string subject, string msg)
        {
            _logger.LogInformation($"从{_mailFrom}给{_mailTo}通过{nameof(CloundMailService)}发送了邮件");
        }
    }
}
