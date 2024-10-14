﻿using SitioVentas.Repository.Helpers.Mappers;
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
        private readonly ISubgrupoService _subgrupoService;
        private readonly IFotoRepository _fotoRepository;
        private readonly IBackupService _backupService;

        public ItemService(IItemRepository itemRepository
                ,IGrupoRepository grupoRepository
                , ISubgrupoService subgrupoService
                , ITipoRepository tipoRepository
                ,IBackupService backupService
                , IFotoRepository fotoRepository) { 
            _itemRepository = itemRepository;
            _fotoRepository = fotoRepository;
            _subgrupoService = subgrupoService;
            _backupService = backupService;
        }

        public async Task<bool> Delete(int Id)
        {
            return await _itemRepository.DeleteLogico(Id);
        }

        public async Task<List<ItemDto>> GetAll()
        {
            List<ItemDto> list = new List<ItemDto>();
            var itemList = (await _itemRepository.GetAllByExpression(x => x.Activo == true)).ToList();
            var subGrupos = (await _subgrupoService.GetAll()).ToList();
            if (itemList != null)
            {
                foreach (var item in itemList)
                {
                    //cargar imagen
                    var fotos = (await _fotoRepository.GetAllByExpression(f => f.ItemId == item.Id)).ToList();
                    ItemDto itemDto = ItemMapper.EntityToDto(item);
                    FotoDto fotoDto;
                    itemDto.Fotos = new List<FotoDto>();
                    var foto = fotos.Find(x => x.Prioridad == 1);
                    if (foto != null)
                    {
                        fotoDto = BuscarFoto(itemDto.Creado, foto);
                        itemDto.Fotos.Add(fotoDto);
                        
                    }
                    //setear subgrupo
                    itemDto.SubgrupoNombre = subGrupos.Find(x => x.Id == item.Subgrupo).Nombre;
                    list.Add(itemDto);
                }
            }
            return list;
        }

        public async Task<List<FotoDto>> GetFotos(int Id)
        {
            List<FotoDto> result = new List<FotoDto>();
            FotoDto fotoDto;
            var item = await _itemRepository.GetById(Id);
            var fotos = (await _fotoRepository.GetAllByExpression(x => x.ItemId == Id)).OrderBy(x => x.Prioridad);
            foreach (var foto in fotos)
            {
                fotoDto = BuscarFoto(item.Creado, foto);
                result.Add(fotoDto);
            }
            return result;
        }

        private FotoDto BuscarFoto(DateTime? fechaCreacion, Foto foto)
        {
            FotoDto fotoDto = ItemMapper.FotoEntityToDto(foto);
            fotoDto.FechaCreacion = fechaCreacion ?? DateTime.Now;
            return _backupService.GetFileDisc(fotoDto);
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
                var itemDto = ItemMapper.EntityToDto(item);
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

        public async Task<ItemDto> Insert(ItemDto item)
        {
            Foto foto;
            var producto = ItemMapper.DtoToEntity(item);
            producto.Creado = DateTime.Now;
            producto.Activo = true;
            producto = await _itemRepository.Insert(producto);
            item.Id = producto.Id;
            foreach (var fotoDto in item.Fotos)
            {
                foto = await GuardarFoto(producto, fotoDto);
            }
            return item;
        }

        public async Task<int> GetTotalNoticias()
        {
            return await _itemRepository.GetCountByExpression(x => x.Activo == true);
        }

        private async Task<Foto> GuardarFoto(Item item, FotoDto fotoDto)
        {
            Foto foto = new Foto();
            fotoDto.ItemId = item.Id;
            fotoDto.FechaCreacion = item.Creado;
            var archRes = _backupService.SaveFileDisk(fotoDto);
            if (archRes.Exitoso)
            {
                fotoDto.Ruta = archRes.Ruta;
                foto = ItemMapper.FotoDtoToEntity(fotoDto);
                var fotoRes = await _fotoRepository.Insert(foto);
            }
            return foto;
        }

        public async Task<ItemDto> Update(int id, ItemDto itemDto)
        {
            Foto foto;
            var objItem = await _itemRepository.GetById(id);
            if (objItem == null) return null;
            itemDto.Creado = objItem.Creado;
            var item = ItemMapper.DtoToEntity(itemDto);
            item.Actualizado = DateTime.Now;
            var result = (await _itemRepository.Update(item));
            if (result != null)
            {
                var fotosBackup = await _fotoRepository.GetAllByExpression(x => x.ItemId == id);
                var cont = 1;
                foreach (var fotoDto in itemDto.Fotos)
                {
                    fotoDto.Prioridad = cont++;
                    foto = await GuardarFoto(item, fotoDto);
                }
                foreach (var fotoDel in fotosBackup)
                {
                    var delFotos = await _fotoRepository.ExecuteCommand("DELETE FROM foto where Id = @Id", new { fotoDel.Id });
                }
            }
            else
            {
                return null;
            }
            return itemDto;
        }
    }
}
