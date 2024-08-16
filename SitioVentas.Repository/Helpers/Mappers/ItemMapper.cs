using System;
using SitioVentas.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using SitioVentas.Dto.Dto;

namespace SitioVentas.Repository.Helpers.Mappers
{
    public class ItemMapper
    {
        public static Item InfoDtoToEntity(ItemDto itemDto)
        {
            Item item = new Item();
            item.Id = itemDto.Id;
            item.Descripcion = itemDto.Description;
            item.Nombre = itemDto.Name;
            item.Tipo = itemDto.Type;

            return item;
        }

        public static ItemDto InfoEntityToDto(Item item)
        {
            ItemDto itemDto = new ItemDto();
            itemDto.Id = item.Id;
            itemDto.Description = item.Descripcion;
            itemDto.Type = item.Tipo;
            itemDto.Name = item.Nombre;

            return itemDto;
        }


    }
}
