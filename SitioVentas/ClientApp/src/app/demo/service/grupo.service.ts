import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IProducto } from '../api/producto.interface';
import { Observable } from 'rxjs';
import { IGrupo } from '../api/grupo';

@Injectable()
export class GrupoService {
    private apiUrl = '/api/grupo'; 
    constructor(private http: HttpClient) { }

    getGrupos(): Observable<IGrupo[]> {
        return this.http.get<IGrupo[]>(this.apiUrl);
    }

}
