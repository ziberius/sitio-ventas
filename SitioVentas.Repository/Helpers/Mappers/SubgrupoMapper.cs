using System;
using SitioVentas.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using SitioVentas.Dto.Dto;

namespace SitioVentas.Repository.Helpers.Mappers
{
    public class SubgrupoMapper
    {
        public static SubGrupo DtoToEntity(SubgrupoDto subgrupoDto)
        {
            SubGrupo subgrupo = new SubGrupo();
            subgrupo.Id = subgrupoDto.Id;
            subgrupo.Nombre = subgrupoDto.Nombre;
            subgrupo.Codigo = subgrupoDto.Codigo;
            subgrupo.GrupoId = subgrupoDto.GrupoId;

            return subgrupo;
        }

        public static SubgrupoDto EntityToDto(SubGrupo subgrupo)
        {
            SubgrupoDto subgrupoDto = new SubgrupoDto();
            subgrupoDto.Id = subgrupo.Id;
            subgrupoDto.Codigo = subgrupo.Codigo;
            subgrupoDto.Nombre = subgrupo.Nombre;
            subgrupoDto.GrupoId = subgrupo.GrupoId;

            return subgrupoDto;
        }

    }
}
