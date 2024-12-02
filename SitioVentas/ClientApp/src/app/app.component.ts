import { Component, OnInit } from '@angular/core';
import { MenuItem, PrimeNGConfig } from 'primeng/api';
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";

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
        const firebaseConfig = {
            apiKey: "AIzaSyC2yNdKLId5tOiHt4hKooM5ZqeZCbA06LE",
            authDomain: "sandra-joyas.firebaseapp.com",
            projectId: "sandra-joyas",
            storageBucket: "sandra-joyas.firebasestorage.app",
            messagingSenderId: "425360206378",
            appId: "1:425360206378:web:2c8e18a72c92847eb9b153",
            measurementId: "G-S9XMWEM6LQ"
        };
        const app = initializeApp(firebaseConfig);
        const analytics = getAnalytics(app);

        this.primengConfig.ripple = true;
    }
}
