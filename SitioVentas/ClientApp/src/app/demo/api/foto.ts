import { SafeUrl } from "@angular/platform-browser";

export interface IFoto {
    id: number;
    nombre: string;
    ruta: string;
    tipo: string;
    prioridad: number;
    archivo: string;
    itemId: number;
    imageUrl: SafeUrl;
}

export class Foto implements IFoto{
    id!: number;
    nombre!: string;
    ruta!: string;
    tipo!: string;
    prioridad!: number;
    archivo!: string;
    itemId!: number;
    imageUrl!: SafeUrl;
}
