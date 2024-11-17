import { SafeUrl } from "@angular/platform-browser";
import { IFoto } from "./foto.interface";

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
