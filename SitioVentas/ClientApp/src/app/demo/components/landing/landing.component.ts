import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { MegaMenuModule } from 'primeng/megamenu';
import { MegaMenuItem, MenuItem, MessageService } from 'primeng/api';
import { ProductService } from '../../service/product.service';
import { Product } from '../../api/product';
import { SubgrupoService } from '../../service/subgrupo.service';
import { IMenuGrupo } from '../../api/menugrupo.interface';

@Component({
    selector: 'app-landing',
    templateUrl: './landing.component.html',
    providers: [MessageService]
})
export class LandingComponent {

    products: Product[] = [];
    productsDestacados: Product[] = [];
    items: MegaMenuItem[] | undefined;
    itemsTmp: MegaMenuItem[] = [];
    item: MenuItem | undefined;
    responsiveOptions: any[] | undefined;
    constructor(public layoutService: LayoutService, public router: Router
        , private subgrupoService: SubgrupoService
        , private messageService: MessageService
        , private productService: ProductService) { }
        

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
        
        this.subgrupoService.getMenu().subscribe({
            next: (data) => {
                data.forEach(menu => {
                    var itemTmp: MegaMenuItem = {};
                    itemTmp.id = menu.id?.toString();
                    itemTmp.label = menu.nombre;
                    var itemSubList: MenuItem[][] = [];
                    menu.menuSubgrupo?.forEach(subgrupo => {
                        var itemSub: MenuItem = {};
                        itemSub.id = subgrupo.id.toString();
                        itemSub.label = subgrupo.nombre;
                        itemSubList.push([itemSub]);
                    });
                    itemTmp.items = itemSubList;
                    this.itemsTmp.push(itemTmp);
                })
                this.items = this.itemsTmp;
            },
            error: (error) => {
                console.log(error);
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al traer tipos de productos', life: 3000 });
            }
        });

    }

    showSubmenu(item: MegaMenuItem) {
        item.visible = true;
    }

    hideSubmenu(item: MegaMenuItem) {
        item.visible = false;
    }
    
}
