using System;
using SitioVentas.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using SitioVentas.Dto.Dto;

namespace SitioVentas.Repository.Helpers.Mappers
{
    public class TipoMapper
    {
        public static Tipo DtoToEntity(TipoDto tipoDto)
        {
            Tipo tipo = new Tipo();
            tipo.Id = tipoDto.Id;
            tipo.Nombre = tipoDto.Nombre;
            tipo.Codigo = tipoDto.Codigo;

            return tipo;
        }

        public static TipoDto EntityToDto(Tipo tipo)
        {
            TipoDto tipoDto = new TipoDto();
            tipoDto.Id = tipo.Id;
            tipoDto.Codigo = tipo.Codigo;
            tipoDto.Nombre = tipo.Nombre;

            return tipoDto;
        }
 
    }
}
