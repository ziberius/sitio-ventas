using System;
using System.Collections.Generic;
using System.Text;

namespace SitioVentas.Dto.Mail
{
    public class MailDto
    {
        public DeDto[] De { get; set; }
        public ParaDto[] Para { get; set; }
        public CCDto[] CC { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public AttachmentDto[] Adjunto { get; set; }
    }
}
