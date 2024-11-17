import { IMenuSubgrupo } from "./menusubgrupo.interface";

export interface IMenuGrupo {
    id?: number;
    codigo?: string;
    nombre?: string;
    menuSubgrupo: IMenuSubgrupo[];
}
