using System;
using System.Collections.Generic;
using System.Text;

namespace SitioVentas.Dto.Mail
{
    public abstract class PersonaMailDto
    {
        public string Nombre { get; set; }
        public string Mail { get; set; }
    }

    public class DeDto : PersonaMailDto
    {
        public DeDto(string Nombre, string Mail)
        {
            this.Nombre = Nombre;
            this.Mail = Mail;
        }
    }

    public class ParaDto : PersonaMailDto
    {
        public ParaDto(string Nombre, string Mail)
        {
            this.Nombre = Nombre;
            this.Mail = Mail;
        }
    }

    public class CCDto : PersonaMailDto
    {
        public CCDto(string Nombre, string Mail)
        {
            this.Nombre = Nombre;
            this.Mail = Mail;
        }
    }
}
