import { Component, OnInit, ViewChild } from '@angular/core';
import { IProducto } from 'src/app/demo/api/producto';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { ProductService } from 'src/app/demo/service/product.service';
import { DomSanitizer } from '@angular/platform-browser';
import { FileUpload } from 'primeng/fileupload';
import { Foto, IFoto } from '../../../api/foto';

@Component({
    templateUrl: './crud.component.html',
    providers: [MessageService]
})
export class CrudComponent implements OnInit {
    @ViewChild('subirFotos')
    fileUpload!: FileUpload;
    productDialog: boolean = false;

    deleteProductDialog: boolean = false;

    deleteProductsDialog: boolean = false;

    products: IProducto[] = [];

    product: IProducto = {};
    itemNew: IProducto = {};
    fotos: IFoto[] = [];

    selectedProducts: IProducto[] = [];

    submitted: boolean = false;

    cols: any[] = [];

    statuses: any[] = [];
    fotosSelect: number = 0;
    rowsPerPageOptions = [5, 10, 20];

    constructor(
        private productService: ProductService
        , private messageService: MessageService
        , private sanitizer: DomSanitizer)
    {

    }

    ngOnInit() {
        this.productService.getProductos().subscribe(data => {
            data.forEach(item => {
                var foto = item.fotos !== undefined?item.fotos[0]:null;
                if (foto) {
                    let objectURL = 'data:' + foto.tipo + ';base64,' + foto.archivo;
                    foto.imageUrl = this.sanitizer.bypassSecurityTrustUrl(objectURL);
                }
            });
            this.products = data
        });

        this.cols = [
            { field: 'product', header: 'Product' },
            { field: 'price', header: 'Price' },
            { field: 'category', header: 'Category' },
            { field: 'rating', header: 'Reviews' },
            { field: 'cantidad', header: 'Cantidad' },
            { field: 'inventoryStatus', header: 'Status' }
        ];

        this.statuses = [
            { label: 'INSTOCK', value: 'instock' },
            { label: 'LOWSTOCK', value: 'lowstock' },
            { label: 'OUTOFSTOCK', value: 'outofstock' }
        ];
    }

    openNew() {
        this.product = {};
        this.submitted = false;
        this.productDialog = true;
    }

    deleteSelectedProducts() {
        this.deleteProductsDialog = true;
    }

    editProduct(product: IProducto) {
        this.product = { ...product };
        this.productDialog = true;
    }

    deleteProduct(product: IProducto) {
        this.deleteProductDialog = true;
        this.product = { ...product };
    }

    confirmDeleteSelected() {
        this.deleteProductsDialog = false;
        this.products = this.products.filter(val => !this.selectedProducts.includes(val));
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Products Deleted', life: 3000 });
        this.selectedProducts = [];
    }

    confirmDelete() {
        this.deleteProductDialog = false;
        this.products = this.products.filter(val => val.id !== this.product.id);
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Product Deleted', life: 3000 });
        this.product = {};
    }

    hideDialog() {
        this.productDialog = false;
        this.submitted = false;
    }

    saveProduct() {
        this.submitted = true;

        if (this.product.nombre?.trim()) {
            if (this.product.id) {
                // @ts-ignore
                this.product.inventoryStatus = this.product.inventoryStatus.value ? this.product.inventoryStatus.value : this.product.inventoryStatus;
                this.products[this.findIndexById(this.product.id)] = this.product;
                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Product Updated', life: 3000 });
            } else {
                this.product.id = this.createId();
                this.product.codigo = this.createId();
                //this.product.image = 'product-placeholder.svg';
                // @ts-ignore
                this.product.inventoryStatus = this.product.inventoryStatus ? this.product.inventoryStatus.value : 'INSTOCK';
                this.products.push(this.product);
                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Product Created', life: 3000 });
            }

            this.products = [...this.products];
            this.productDialog = false;
            this.product = {};
        }
    }

    findIndexById(id: string): number {
        let index = -1;
        for (let i = 0; i < this.products.length; i++) {
            if (this.products[i].id === id) {
                index = i;
                break;
            }
        }

        return index;
    }

    createId(): string {
        let id = '';
        const chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        for (let i = 0; i < 5; i++) {
            id += chars.charAt(Math.floor(Math.random() * chars.length));
        }
        return id;
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    subirFoto(event: any) {
        if (this.itemNew.fotos !== undefined) {
            this.fotos = this.itemNew.fotos;
        }
        
        var foto: IFoto;
        let contArchivos = event.files.length + this.itemNew.fotos?.length;
        for (const [index, file] of event.files.entries()) {
            let fileReader = new FileReader();
            fileReader.readAsDataURL(file);
            fileReader.onload = () => {
                // Will print the base64 here.
                foto = new Foto();
                foto.nombre = file.name;
                foto.tipo = file.type;
                foto.archivo = "";
                if (fileReader.result !== null) {
                    foto.archivo = fileReader.result.toString().replace('data:', '').replace(/^.+,/, '');
                }
                foto.prioridad = index + 1;
                this.fotos.push(foto);
                if (this.fotos.length == contArchivos) {
                    this.fotosSelect = 0;
                    this.itemNew.fotos = this.fotos;
                    this.fileUpload.clear();
                    this.messageService.add({ severity: 'success', summary: 'Procesar', detail: 'Archivos procesados exitosamente.', life: 3000 });
                }
            };
        }
    }
    eliminarFotos() {
        this.fotosSelect = 0;
    }

    eliminarFoto(event:any) {
        this.fotosSelect = this.fileUpload.files.length - 1;
    }

    seleccionarFotos(event:any) {
        if (event.files.length + this.itemNew.fotos?.length > 3) {
            this.messageService.add({ severity: 'error', summary: 'Cantidad excedida', detail: 'Solo puedes cargar máximo tres fotos.', life: 3000 });
            this.fileUpload.clear();
        } else {
            this.fotosSelect = event.files.length;
        }
    }
}
