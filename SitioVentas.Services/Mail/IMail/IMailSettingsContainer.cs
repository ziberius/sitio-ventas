using SitioVentas.Dto.Mail.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace SitioVentas.Services.Mail.IMail
{
    public interface IMailSettingsContainer
    {
        MailSettings GetMailSettings();
    }
}
