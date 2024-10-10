import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IProducto } from '../api/producto.interface';
import { Observable } from 'rxjs';
import { ISubgrupo } from '../api/subgrupo';

@Injectable()
export class SubgrupoService {
    private apiUrl = '/api/subgrupo'; 
    constructor(private http: HttpClient) { }

    getSubgrupos(): Observable<ISubgrupo[]> {
        return this.http.get<ISubgrupo[]>(this.apiUrl);
    }

}
