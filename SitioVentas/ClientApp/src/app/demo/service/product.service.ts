import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IProducto } from '../api/producto.interface';
import { Observable } from 'rxjs';
import { Product } from '../api/product';
import { IFoto } from '../api/foto';

@Injectable()
export class ProductService {
    private apiUrl = '/api/item'; 
    constructor(private http: HttpClient) { }

    getProductsSmall() {
        return this.http.get<any>('assets/demo/data/products-small.json')
            .toPromise()
            .then(res => res.data as Product[])
            .then(data => data);
    }

    getProductos(): Observable<IProducto[]> {
        return this.http.get<IProducto[]>(this.apiUrl);
    }

    getFotosProducto(id: number): Observable<IFoto[]> {
        const URL = `${this.apiUrl}/fotos/${id}`;
        return this.http.get<IFoto[]>(URL);
    }

    getProducts() {
        return this.http.get<any>('assets/demo/data/products.json')
            .toPromise()
            .then(res => res.data as Product[])
            .then(data => data);
    }

    getProductsMixed() {
        return this.http.get<any>('assets/demo/data/products-mixed.json')
            .toPromise()
            .then(res => res.data as Product[])
            .then(data => data);
    }

    getProductsWithOrdersSmall() {
        return this.http.get<any>('assets/demo/data/products-orders-small.json')
            .toPromise()
            .then(res => res.data as Product[])
            .then(data => data);
    }

    createProduct(data: IProducto): Observable<IProducto> {
        return this.http.post<IProducto>(this.apiUrl, data, { params: { blockui: true.toString() } });
    }

    updateProduct(data: IProducto): Observable<HttpResponse<IProducto>> {
        const URL = `${this.apiUrl}/${data.id}`;
        return this.http.put<HttpResponse<IProducto>>(URL, data, { params: { blockui: true.toString() } });
    }

    deleteProduct(id: number): Observable<IProducto> {
        const URL = `${this.apiUrl}/${id}`;
        return this.http.delete<IProducto>(URL);
    }
}
