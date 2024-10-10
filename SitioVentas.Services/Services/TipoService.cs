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
    public class TipoService: ITipoService
    {
        private readonly ITipoRepository _tipoRepository;

        public TipoService(ITipoRepository tipoRepository
        ) { 
            _tipoRepository = tipoRepository;
        }

        public Task<bool> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<TipoDto> Get(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TipoDto>> GetAll()
        {
            var list =  (await _tipoRepository.GetAll()).ToList();
            List<TipoDto> listDto = new List<TipoDto>();
            TipoDto tipoDto = new TipoDto();
            foreach(var tipo in list)
            {
                tipoDto = TipoMapper.EntityToDto(tipo);
                listDto.Add(tipoDto);
            }
            return listDto;
        }

        public Task<TipoDto> Insert(TipoDto tipo)
        {
            throw new NotImplementedException();
        }

        public Task<TipoDto> Update(int id, TipoDto tipo)
        {
            throw new NotImplementedException();
        }
    }
}
