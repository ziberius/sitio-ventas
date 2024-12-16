import { Component } from '@angular/core';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { ILoginRequest } from '../../../api/loginrequest.interface';
import { AuthService } from '../../../service/auth.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styles: [`
        :host ::ng-deep .pi-eye,
        :host ::ng-deep .pi-eye-slash {
            transform:scale(1.6);
            margin-right: 1rem;
            color: var(--primary-color) !important;
        }
    `]
})
export class LoginComponent {

    credentials: ILoginRequest = {
        username: '',
        password: ''
    };

    valCheck: string[] = ['remember'];

    password!: string;

    constructor(
        private authService: AuthService,
        private router: Router,
        public layoutService: LayoutService
    ) { }

    login(): void {
        this.authService.login(this.credentials)
            .subscribe({
                next: (response) => {
                    console.log('Login exitoso', response);
                    this.router.navigate(['/pages/crud']);
                },
                error: (error) => {
                    console.error('Error en login', error);
                    // Maneja el error (muestra mensaje, etc.)
                }
            });
    }

}
