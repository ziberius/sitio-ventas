import { IFoto } from "../api/foto";
import { IProducto } from "../api/producto.interface";


export class Producto implements IProducto {
    id: number;
    codigo?: string;
    nombre?: string;
    descripcion?: string;
    precio?: number;
    subgrupo?: number;
    subgrupoNombre?: string;
    cantidad?: number;
    tipo?: number;
    fotos: IFoto[];

    constructor(data?: IProducto) {
        this.id = data?.id ? data.id : 0;
        this.codigo = data?.codigo ? data.codigo : '';
        this.nombre = data?.nombre ? data.nombre : '';
        this.descripcion = data?.descripcion ? data.descripcion : '';
        this.precio = data?.precio ? data.precio : 0;
        this.subgrupo = data?.subgrupo ? data.subgrupo : 0;
        this.subgrupoNombre = data?.subgrupoNombre ? data.subgrupoNombre : '';
        this.cantidad = data?.cantidad ? data.cantidad : 0;
        this.tipo = data?.tipo ? data.tipo : 0;

        this.fotos = [];
    }

}
