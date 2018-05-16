using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.WebApi.Interfaces
{
    public interface IMailService
    {
        void Send(string subject, string msg);
    }
}
