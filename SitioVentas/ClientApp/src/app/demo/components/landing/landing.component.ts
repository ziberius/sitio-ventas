import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { MegaMenuModule } from 'primeng/megamenu';
import { MegaMenuItem, MenuItem } from 'primeng/api';
import { ProductService } from '../../service/product.service';
import { Product } from '../../api/product';

@Component({
    selector: 'app-landing',
    templateUrl: './landing.component.html'
})
export class LandingComponent {

    products: Product[] = [];
    productsDestacados: Product[] = [];
    items: MegaMenuItem[] | undefined;
    item: MenuItem | undefined;
    responsiveOptions: any[] | undefined;
    constructor(public layoutService: LayoutService, public router: Router,
        private productService: ProductService) { }
        

    ngOnInit() {
        this.responsiveOptions = [
            {
                breakpoint: '1400px',
                numVisible: 3,
                numScroll: 3
            },
            {
                breakpoint: '1220px',
                numVisible: 2,
                numScroll: 2
            },
            {
                breakpoint: '1100px',
                numVisible: 1,
                numScroll: 1
            }
        ];

        this.productService.getProducts().then(data =>
            this.products = data
        );

        this.productService.getProductsSmall().then(data =>
            this.productsDestacados = data
        );



        this.items = [
            {
                label: 'Clásicos',
                items: [[
                        {label: 'Conjuntos'},
                        {label: 'Solitados'},
                        {label: '1/2 Cintilla'},
                        {label: 'Aros Argolla' }
                ]]

            },
            {
                label: 'Símbolos',
                items: [[
                        {label: 'Flor Loto'},
                        {label: 'Flor de la Vida'},
                        {label: 'Rosa de los Vientos'},
                        {label: 'Mandela'}
                ]]
            },
            {
                label: 'Protección',
                items: [[
                        {label: 'Nudo de Brujas'},
                        {label: 'San Benito'},
                        {label: 'Triqueta'},
                        {label: 'Tetragramatón'}
                ]], visible: false
            },
            {
                label: 'Colecciones',
                items: [[
                        {label: 'Lunas'},
                        {label: 'Llamadora de Anel'},
                        {label: 'Zodiaco'},
                        {label: 'Trepadores'}
                ]], visible: false
            },
            {
                label: 'Accesorios',
                items: [[
                        {label: 'Colares Tela'},
                        {label: 'Bordado'}
                ]], visible: false
            }
        ];
    }

    showSubmenu(item: MegaMenuItem) {
        item.visible = true;
    }

    hideSubmenu(item: MegaMenuItem) {
        item.visible = false;
    }
    
}
