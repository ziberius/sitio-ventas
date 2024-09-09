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
    public class ItemService: IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IGrupoRepository _grupoRepository;
        private readonly ISubGrupoRepository _subgrupoRepository;
        private readonly ITipoRepository _tipoRepository;
        private readonly IFotoRepository _fotoRepository;
        private readonly IBackupService _backupService;

        public ItemService(IItemRepository itemRepository
                ,IGrupoRepository grupoRepository
                ,ISubGrupoRepository subGrupoRepository
                ,ITipoRepository tipoRepository
                ,IBackupService backupService
                , IFotoRepository fotoRepository) { 
            _itemRepository = itemRepository;
            _fotoRepository = fotoRepository;
            _tipoRepository = tipoRepository;
            _grupoRepository = grupoRepository;
            _subgrupoRepository = subGrupoRepository;
            _backupService = backupService;
        }

        public Task<bool> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemDto> Get(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ItemDto>> GetAll()
        {
            List<ItemDto> list = new List<ItemDto>();
            var itemList = (await _itemRepository.GetAll()).ToList();
            if (itemList != null)
            {
                foreach (var item in itemList)
                {
                    var fotos = (await _fotoRepository.GetAllByExpression(f => f.ItemId == item.Id)).ToList();
                    ItemDto itemDto = ItemMapper.InfoEntityToDto(item);
                    FotoDto fotoDto;
                    itemDto.Fotos = new List<FotoDto>();
                    var foto = fotos.Find(x => x.Prioridad == 1);
                    if (foto != null)
                    {
                        fotoDto = BuscarFoto(itemDto.Creado, foto);
                        itemDto.Fotos.Add(fotoDto);
                        
                    }
                    list.Add(itemDto);
                }
            }
            return list;
        }

        public Task<List<FotoDto>> GetFotos(int Id)
        {
            throw new NotImplementedException();
        }

        private FotoDto BuscarFoto(DateTime? fechaCreacion, Foto foto)
        {
            FotoDto fotoDto = ItemMapper.FotoEntityToDto(foto);
            ArchivoDto archivo = new ArchivoDto();
            archivo = ItemMapper.FotoDtoToArchivoFtp(fotoDto);
            archivo.FechaCreacion = fechaCreacion ?? DateTime.Now;
            archivo = _backupService.GetFileDisc(archivo);
            fotoDto.Archivo = archivo.ContenidoArchivoB64;
            return fotoDto;
        }

        public async Task<List<ItemDto>> GetPaginated(int pageNumber, int pageSize)
        {
            int offset = (pageNumber - 1) * pageSize;

            string sql = "SELECT * FROM Item Where Activo = 1 ORDER BY Actualizado DESC, Creado desc LIMIT @PageSize OFFSET @Offset";

            var items = await _itemRepository.ExecutedQuery(sql, new { Offset = offset, PageSize = pageSize });

            List<ItemDto> itemListDto = new List<ItemDto>();
            foreach (var item in items)
            {
                var fotos = (await _fotoRepository.GetAllByExpression(f => f.ItemId == item.Id)).ToList();
                var itemDto = ItemMapper.InfoEntityToDto(item);
                FotoDto fotoDto;
                itemDto.Fotos = new List<FotoDto>();
                var foto = fotos.Find(x => x.Prioridad == 1);
                if (foto == null)
                {
                    itemListDto.Add(itemDto);
                }
                else
                {
                    fotoDto = BuscarFoto(itemDto.Creado, foto);
                    itemDto.Fotos.Add(fotoDto);
                    itemListDto.Add(itemDto);
                }
            }

            return itemListDto;
        }

        public Task<List<ItemDto>> GetPaginatedFiltered(PaginatedFilteredRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<ItemDto> Insert(ItemDto item)
        {
            throw new NotImplementedException();
        }

        public Task<ItemDto> Update(int id, ItemDto item)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalNoticias()
        {
            return await _itemRepository.GetCountByExpression(x => x.Activo == true);
        }
    }
}
