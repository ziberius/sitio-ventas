using System;
using SitioVentas.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using SitioVentas.Dto.Dto;

namespace SitioVentas.Repository.Helpers.Mappers
{
    public class GrupoMapper
    {
        public static Grupo DtoToEntity(GrupoDto grupoDto)
        {
            Grupo grupo = new Grupo();
            grupo.Id = grupoDto.Id;
            grupo.Nombre = grupoDto.Nombre;
            grupo.Codigo = grupoDto.Codigo;

            return grupo;
        }

        public static GrupoDto EntityToDto(Grupo grupo)
        {
            GrupoDto grupoDto = new GrupoDto();
            grupoDto.Id = grupo.Id;
            grupoDto.Codigo = grupo.Codigo;
            grupoDto.Nombre = grupo.Nombre;

            return grupoDto;
        }
 
    }
}
