// services/auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { ILoginRequest } from '../api/loginrequest.interface';
import { ILoginResponse } from '../api/loginresponse.interface';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private apiUrl = '/api/auth'; 

    constructor(private http: HttpClient) { }

    login(credentials: ILoginRequest): Observable<ILoginResponse> {
        return this.http.post<ILoginResponse>(`${this.apiUrl}/login`, credentials)
            .pipe(
                tap(response => {
                    // Guarda el token en localStorage
                    if (response.token) {
                        localStorage.setItem('token', response.token);
                        localStorage.setItem('usuario', JSON.stringify(response.usuario));
                    }
                })
            );
    }

    logout(): void {
        localStorage.removeItem('token');
        localStorage.removeItem('usuario');
    }

    isAuthenticated(): boolean {
        return !!localStorage.getItem('token');
    }

    getToken(): string | null {
        return localStorage.getItem('token');
    }
}
