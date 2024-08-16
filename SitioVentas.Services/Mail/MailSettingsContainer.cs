using SitioVentas.Dto.Mail.Settings;
using SitioVentas.Services.Mail.IMail;
using System;
using System.Collections.Generic;
using System.Text;

namespace SitioVentas.Services.Mail
{
    public class MailSettingsContainer : IMailSettingsContainer
    {

        private readonly MailSettings mailSettings;

        public MailSettingsContainer(MailSettings mailSettings)
        {
            this.mailSettings = mailSettings;
        }

        public MailSettings GetMailSettings()
        {
            return mailSettings;
        }
    }
}
