using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SitioVentas.Constants.Constants;
using SitioVentas.Dto.Dto;

namespace SitioVentas.Services.IServices
{
    public interface IBackupService
    {
        SaveFileResult SaveFileDisk(ArchivoDto arch);
        ArchivoDto GetFileDisc(ArchivoDto arch);
    }
}
