import { Component, OnInit } from '@angular/core';
import { MenuItem, PrimeNGConfig } from 'primeng/api';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
    menuItems: MenuItem[] = [
        {
            label: 'Inicio',
            items: [
                { label: 'Submenú 1.1' },
                { label: 'Submenú 1.2' }
            ]
        },
        {
            label: 'Productos',
            items: [
                { label: 'Submenú 2.1' },
                { label: 'Submenú 2.2' }
            ]
        }
    ];
    constructor(private primengConfig: PrimeNGConfig) { }

    ngOnInit() {
        this.primengConfig.ripple = true;
    }
}
