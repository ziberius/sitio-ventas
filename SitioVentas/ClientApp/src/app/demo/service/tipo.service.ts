import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ITipo } from '../api/tipo.interface';

@Injectable()
export class TipoService {
    private apiUrl = '/api/tipo'; 
    constructor(private http: HttpClient) { }

    getTipos(): Observable<ITipo[]> {
        return this.http.get<ITipo[]>(this.apiUrl);
    }

}
