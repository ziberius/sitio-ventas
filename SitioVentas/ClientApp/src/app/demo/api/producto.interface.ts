import { IFoto } from './foto';

export interface IProducto {
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
}
