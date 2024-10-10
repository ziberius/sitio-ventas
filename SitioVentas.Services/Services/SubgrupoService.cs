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
    public class SubgrupoService: ISubgrupoService
    {
        private readonly ISubGrupoRepository _subgrupoRepository;

        public SubgrupoService(ISubGrupoRepository subgrupoRepository
        ) { 
            _subgrupoRepository = subgrupoRepository;
        }

        public Task<bool> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<SubgrupoDto> Get(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SubgrupoDto>> GetAll()
        {
            var list =  (await _subgrupoRepository.GetAll()).ToList();
            List<SubgrupoDto> listDto = new List<SubgrupoDto>();
            SubgrupoDto subgrupoDto = new SubgrupoDto();
            foreach(var subgrupo in list)
            {
                subgrupoDto = SubgrupoMapper.EntityToDto(subgrupo);
                listDto.Add(subgrupoDto);
            }
            return listDto;
        }

        public Task<SubgrupoDto> Insert(SubgrupoDto grupo)
        {
            throw new NotImplementedException();
        }

        public Task<SubgrupoDto> Update(int id, SubgrupoDto grupo)
        {
            throw new NotImplementedException();
        }
    }
}
