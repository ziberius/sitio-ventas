using SitioVentas.Repository.Helpers.Mappers;
using SitioVentas.Dto.Dto;
using SitioVentas.Repository.IRepository;
using SitioVentas.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SitioVentas.Entities.Entities;

namespace SitioVentas.Services.Services
{
    public class GrupoService: IGrupoService
    {
        private readonly IGrupoRepository _grupoRepository;

        public GrupoService(IGrupoRepository grupoRepository
        ) { 
            _grupoRepository = grupoRepository;
        }

        public Task<bool> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GrupoDto> Get(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GrupoDto>> GetAll()
        {
            var list =  (await _grupoRepository.GetAllByExpression(x => x.Activo == true)).ToList();
            List<GrupoDto> listDto = new List<GrupoDto>();
            GrupoDto grupoDto = new GrupoDto();
            foreach(var grupo in list)
            {
                grupoDto = GrupoMapper.EntityToDto(grupo);
                listDto.Add(grupoDto);
            }
            return listDto;
        }

        public Task<GrupoDto> Insert(GrupoDto grupo)
        {
            throw new NotImplementedException();
        }

        public Task<GrupoDto> Update(int id, GrupoDto grupo)
        {
            throw new NotImplementedException();
        }
    }
}
