using SitioVentas.Services.IServices;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using SitioVentas.Dto.Dto;
using System.Collections.Generic;
using SitioVentas.Constants.Constants;
using System.Linq;
using System.Text;

namespace SitioVentas.Services.Services
{
    public class BackupService : IBackupService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        private Dictionary<int, string> meses = new Dictionary<int, string>
        {
            {1,FechasConstants.ENERO_NOMBRE },
            {2,FechasConstants.FEBRERO_NOMBRE },
            {3,FechasConstants.MARZO_NOMBRE },
            {4,FechasConstants.ABRIL_NOMBRE },
            {5,FechasConstants.MAYO_NOMBRE },
            {6,FechasConstants.JUNIO_NOMBRE },
            {7,FechasConstants.JULIO_NOMBRE },
            {8,FechasConstants.AGOSTO_NOMBRE },
            {9,FechasConstants.SEPTIEMBRE_NOMBRE },
            {10,FechasConstants.OCTUBRE_NOMBRE },
            {11,FechasConstants.NOVIEMBRE_NOMBRE },
            {12,FechasConstants.DICIEMBRE_NOMBRE }
        };

        public BackupService(ILogger<BackupService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }


        private string GenerarRutaDisco(DateTime fecha)
        {
            return _configuration.GetSection(FileConstants.FILE_LOCAL)[FileConstants.FILE_LOCAL_UNIT] + Path.DirectorySeparatorChar
                + _configuration.GetSection(FileConstants.FILE_LOCAL)[FileConstants.APPSETTINGS_DIR]
                + Path.DirectorySeparatorChar + fecha.Year.ToString()
                + Path.DirectorySeparatorChar + meses.GetValueOrDefault(fecha.Month)
                + Path.DirectorySeparatorChar + fecha.Day.ToString() + Path.DirectorySeparatorChar
                + _configuration.GetSection(FileConstants.FILE_LOCAL)[FileConstants.APPSETTINGS_DIR_TIPO];

        }


        private string CrearRutaDisco(string ruta, ArchivoDto arch)
        {
            string rutaFinal = ruta + Path.DirectorySeparatorChar + arch.ItemId.ToString();
            if (!Directory.Exists(rutaFinal))
            {
                Directory.CreateDirectory(rutaFinal);
            }
            return rutaFinal;
        }


        public SaveFileResult SaveFileDisk(ArchivoDto arch)
        {
            _logger.LogInformation("**** GUARDANDO ARCHIVO *****");
            _logger.LogInformation($"[BackupService] GUARDANDO ARCHIVO NOMBRE: {arch.NombreArchivo}");
            SaveFileResult result = new SaveFileResult() { Ruta = "", Exitoso = false };
            string rutaDisco = "";
            try
            {
                rutaDisco = GenerarRutaDisco(arch.FechaCreacion);
                string rutaFinal;
                rutaFinal = CrearRutaDisco(rutaDisco, arch);
                var archivo = Convert.FromBase64String(arch.ContenidoArchivoB64);
                File.WriteAllBytes(rutaFinal + Path.DirectorySeparatorChar + arch.NombreArchivo, archivo);
                result.Exitoso = true;
                result.Ruta = rutaFinal;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                result.Exitoso = false;
                return result;
            }
            _logger.LogInformation("**** FINALIZACIÓN GUARNDANDO ARCHIVO LOCAL*****");
            return result;
        }

        public ArchivoDto GetFileDisc(ArchivoDto arch)
        {
            _logger.LogInformation("**** DESCARGANDO ARCHIVO *****");
            _logger.LogInformation($"[BackupService] BUSCANDO ARCHIVO LOCAL NOMBRE: {arch.NombreArchivo}");
            try
            {
                var archivo = File.ReadAllBytes(arch.Ruta + Path.DirectorySeparatorChar + arch.NombreArchivo);
                arch.ContenidoArchivoB64 = Convert.ToBase64String(archivo);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return arch;
            }
            _logger.LogInformation("**** FINALIZACIÓN BUSCANDO ARCHIVO LOCAL*****");
            return arch;
        }

 
    }
}
