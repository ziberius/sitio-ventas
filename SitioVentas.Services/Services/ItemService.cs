using SitioVentas.Repository.Helpers.Mappers;
using SitioVentas.Services.Base;
using SitioVentas.Dto.Dto;
using SitioVentas.Repository.Helpers.Mappers;
using SitioVentas.Repository.IRepository;
using SitioVentas.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitioVentas.Services.Services
{
    public class ItemService: IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository) { 
            _itemRepository = itemRepository;
        }
        public async Task<List<ItemDto>> GetAll()
        {
            List<ItemDto> list = new List<ItemDto>();
            var itemList = (await _itemRepository.GetAll()).ToList();
            if (itemList != null)
            {
                List<ItemDto> listDto = new List<ItemDto>();
                foreach (var item in itemList)
                {
                    ItemDto itemDto = new ItemDto();
                    itemDto = ItemMapper.InfoEntityToDto(item);
                    list.Add(itemDto);
                }
            }
            return list;
        }
    }
}
