using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using AspNetCore.WebApi.Interfaces;

namespace AspNetCore.WebApi.Services
{
    public class CloudMailService : IMailService
    {
        private string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
        private string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];
        private readonly ILogger<CloudMailService> _logger;

        public CloudMailService(ILogger<CloudMailService> logger)
        {
            this._logger = logger;
        }

        public void Send(string subject, string msg)
        {
            _logger.LogInformation($"从{_mailFrom}给{_mailTo}通过{nameof(CloudMailService)}发送了邮件");
        }
    }
}
