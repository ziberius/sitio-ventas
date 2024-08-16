using SitioVentas.Dto.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SitioVentas.Services.Mail.IMail
{
    public interface IMailService
    {
        Task EnviarMailAsync(MailDto email);
    }
}
