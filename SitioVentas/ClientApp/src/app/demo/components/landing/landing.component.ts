import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { MegaMenuModule } from 'primeng/megamenu';
import { MegaMenuItem, MenuItem, MessageService } from 'primeng/api';
import { ProductService } from '../../service/product.service';
import { Product } from '../../api/product';
import { SubgrupoService } from '../../service/subgrupo.service';
import { IMenuGrupo } from '../../api/menugrupo.interface';
import { IProducto } from '../../api/producto.interface';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { SidebarModule } from 'primeng/sidebar';
import { ToastModule } from 'primeng/toast';
import { DialogModule } from 'primeng/dialog';
import { GalleriaModule } from 'primeng/galleria';

@Component({
    selector: 'app-landing',
    templateUrl: './landing.component.html',
    providers: [MessageService]
})
export class LandingComponent {

    products: IProducto[] = [];
    productsDestacados: IProducto[] = [];
    items: MegaMenuItem[] | undefined;
    itemsTmp: MegaMenuItem[] = [];
    item: MenuItem | undefined;
    responsiveOptions: any[] | undefined;
    cartSidebarVisible: boolean = false;
    cartItems: any[] = [];
    productDialogVisible: boolean = false;
    selectedProduct: IProducto = { id:0, fotos:[] };
    galleriaResponsiveOptions: any[] = [
        {
            breakpoint: '1024px',
            numVisible: 5
        },
        {
            breakpoint: '768px',
            numVisible: 3
        },
        {
            breakpoint: '560px',
            numVisible: 1
        }
    ];

    constructor(public layoutService: LayoutService, public router: Router
        , private subgrupoService: SubgrupoService
        , private messageService: MessageService
        , private productService: ProductService
        , private sanitizer: DomSanitizer) { }
        

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

        this.productService.getProductsDestacados().subscribe({
            next: (data) => {
                data.forEach(item => {
                    var foto = item.fotos !== undefined ? item.fotos[0] : null;
                    if (foto) {
                        let objectURL = 'data:' + foto.tipo + ';base64,' + foto.archivo;
                        foto.imageUrl = this.sanitizer.bypassSecurityTrustUrl(objectURL);
                    }
                    this.productsDestacados = data;
                });
            },
            error: (error) => {
                console.log(error);
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al traer productos destacados', life: 3000 });
            }
        });

                

        this.subgrupoService.getSubgrupos().subscribe(data => {

        })
       
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

    addToCart(product: any) {
        const existingItem = this.cartItems.find(item => item.id === product.id);
        
        if (existingItem) {
            if (existingItem.cartQuantity < existingItem.cantidad) {
                existingItem.cartQuantity++;
                this.messageService.add({
                    severity: 'success',
                    summary: 'Producto actualizado',
                    detail: `Se agregó una unidad más de ${product.nombre}`
                });
            } else {
                this.messageService.add({
                    severity: 'warn',
                    summary: 'Límite alcanzado',
                    detail: `No hay más unidades disponibles de ${product.nombre}`
                });
            }
        } else {
            this.cartItems.push({
                ...product,
                cartQuantity: 1
            });
            this.messageService.add({
                severity: 'success',
                summary: 'Producto agregado',
                detail: `${product.nombre} fue agregado al carrito`
            });
        }
    }

    increaseQuantity(item: any) {
        if (item.cartQuantity < item.cantidad) {
            item.cartQuantity++;
        }
    }

    decreaseQuantity(item: any) {
        if (item.cartQuantity > 1) {
            item.cartQuantity--;
        }
    }

    removeFromCart(product: any) {
        const index = this.cartItems.findIndex(item => item.id === product.id);
        if (index > -1) {
            this.cartItems.splice(index, 1);
        }
    }

    showCart() {
        this.cartSidebarVisible = true;
    }

    getTotal() {
        return this.cartItems.reduce((sum, item) => sum + (item.precio * item.cartQuantity), 0);
    }

    showProductDetails(product: IProducto) {
        this.selectedProduct = product;

        this.productService.getFotosProducto(product.id).subscribe({
            next: (data) => {
                data.forEach(foto => {
                    let objectURL = 'data:' + foto.tipo + ';base64,' + foto.archivo;
                    foto.imageUrl = this.sanitizer.bypassSecurityTrustUrl(objectURL);
                    this.selectedProduct.fotos.push(foto);
                });
            }
        });

        this.productDialogVisible = true;
    }
}
