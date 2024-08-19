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
            itemDto.Creado = item.Creado;
            itemDto.Activo = item.Activo;

            return itemDto;
        }


        public static FotoDto FotoEntityToDto(Foto foto)
        {
            FotoDto fotoDto = new FotoDto();
            fotoDto.Id = foto.Id;
            fotoDto.Nombre = foto.Nombre;
            fotoDto.Ruta = foto.Ruta;
            fotoDto.Prioridad = foto.Prioridad;
            fotoDto.Tipo = foto.Tipo;
            fotoDto.ItemId = foto.ItemId;

            return fotoDto;
        }

        public static Foto FotoDtoToEntity(FotoDto fotoDto)
        {
            Foto foto = new Foto();
            foto.Id = fotoDto.Id;
            foto.Nombre = fotoDto.Nombre;
            foto.Ruta = fotoDto.Ruta;
            foto.Prioridad = fotoDto.Prioridad;
            foto.Tipo = fotoDto.Tipo;
            foto.ItemId = fotoDto.ItemId;

            return foto;
        }

        public static ArchivoDto FotoDtoToArchivoFtp(FotoDto fotoDto)
        {
            var archivo = new ArchivoDto();
            archivo.NombreArchivo = fotoDto.Nombre;
            archivo.Extension = fotoDto.Tipo;
            archivo.Ruta = fotoDto.Ruta;
            archivo.ContenidoArchivoB64 = fotoDto.Archivo;
            archivo.ItemId = fotoDto.ItemId;

            return archivo;
        }
    }
}
