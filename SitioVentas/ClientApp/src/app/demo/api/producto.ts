import { IFoto } from './foto';

export interface IProducto {
    id?: string;
    codigo?: string;
    nombre?: string;
    descripcion?: string;
    precio?: number;
    subgrupo?: string;
    fotos?: IFoto[];
}
