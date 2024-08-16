using SitioVentas.Dto.Mail;
using SitioVentas.Dto.Mail.Settings;
using SitioVentas.Services.Mail.IMail;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ServiciosIntraUmar.Services.Mail
{
    public class MailService: IMailService
    {
        private readonly MailSettings mailSettings;

        public MailService(IMailSettingsContainer mailSettingsContainer)
        {
            this.mailSettings = mailSettingsContainer.GetMailSettings();
        }

        public async Task EnviarMailAsync(MailDto emailDto)
        {
            if (emailDto == null)
            {
                throw new ArgumentException("emailDto nulo");
            }
            if (emailDto.De == null || emailDto.De.Length == 0)
            {
                throw new ArgumentException("Campo De vacio");
            }
            if (emailDto.Para == null || emailDto.Para.Length == 0)
            {
                throw new ArgumentException("Campo Para vacio");
            }
            if (string.IsNullOrEmpty(emailDto.Asunto))
            {
                throw new ArgumentException("Campo Asunto vacio");
            }
            if (string.IsNullOrEmpty(emailDto.Mensaje))
            {
                throw new ArgumentException("Campo Mensaje vacio");
            }

            MimeMessage email = new MimeMessage();

            email.From.AddRange(emailDto.De.Select(x => new MailboxAddress(x.Nombre, x.Mail)));
            email.To.AddRange(emailDto.Para.Select(x => new MailboxAddress(x.Nombre, x.Mail)));
            if (emailDto.CC != null && emailDto.CC.Length > 0)
            {
                email.Cc.AddRange(emailDto.CC.Select(x => new MailboxAddress(x.Nombre, x.Mail)));
            }


            email.Subject = emailDto.Asunto;

            var body = new BodyBuilder
            {
                HtmlBody = emailDto.Mensaje
            };

            if (emailDto.Adjunto != null && emailDto.Adjunto.Length > 0)
            {
                foreach (var attachment in emailDto.Adjunto)
                {
                    using (var stream = await attachment.ContentToStreamAsync())
                    {
                        body.Attachments.Add(attachment.FileName, stream);
                    }
                }
            }

            email.Body = body.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                await client.ConnectAsync(
                    mailSettings.Host,
                    mailSettings.Port,
                    mailSettings.UseSsl)
                    .ConfigureAwait(false);
                await client.AuthenticateAsync(
                   mailSettings.UserName,
                    mailSettings.Password).ConfigureAwait(false);

                await client.SendAsync(email).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
