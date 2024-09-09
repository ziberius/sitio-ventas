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
            item.Descripcion = itemDto.Descripcion;
            item.Nombre = itemDto.Nombre;
            item.Tipo = itemDto.Tipo;

            return item;
        }

        public static ItemDto InfoEntityToDto(Item item)
        {
            ItemDto itemDto = new ItemDto();
            itemDto.Id = item.Id;
            itemDto.Descripcion = item.Descripcion;
            itemDto.Tipo = item.Tipo;
            itemDto.Nombre = item.Nombre;
            itemDto.Codigo = item.Codigo;
            itemDto.Creado = item.Creado;
            itemDto.Activo = item.Activo;
            itemDto.Precio = item.Precio;
            itemDto.Cantidad = item.Cantidad;

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
