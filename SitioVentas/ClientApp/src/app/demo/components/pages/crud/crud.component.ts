import { Component, OnInit, ViewChild } from '@angular/core';
import { IProducto } from 'src/app/demo/api/producto.interface';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { ProductService } from 'src/app/demo/service/product.service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { FileUpload } from 'primeng/fileupload';
import { Foto, IFoto } from '../../../api/foto';
import { IGrupo } from '../../../api/grupo';
import { ISubgrupo } from '../../../api/subgrupo';
import { GrupoService } from '../../../service/grupo.service';
import { SubgrupoService } from '../../../service/subgrupo.service';
import { environment } from 'src/environments/environment';
import { Producto } from '../../../model/producto.model';
import { TipoService } from '../../../service/tipo.service';
import { ITipo } from '../../../api/tipo.interface';
import { concatMap, from } from 'rxjs';

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
    grupos: IGrupo[] = [];
    subgrupos: ISubgrupo[] = [];
    subgruposTotal: ISubgrupo[] = [];
    tipoProductos: ITipo[] = [];

    selectedGrupo: IGrupo = {};
    selectedSubgrupo: ISubgrupo = {};
    selectedTipo: ITipo = {};

    product: Producto = new Producto();

    itemNew: Producto = new Producto();
    fotos: IFoto[] = [];

    selectedProducts: IProducto[] = [];
    fotosSelect: number = 0;

    submitted: boolean = false;

    cols: any[] = [];

    statuses: any[] = [];
    rowsPerPageOptions = [5, 10, 20];

    editando: Boolean = false;

    public maxSize: number = 0;

    displayFotos: boolean = false;
    currentFoto: Foto = new Foto();

    constructor(
        private productService: ProductService
        , private grupoService: GrupoService
        , private subgrupoService: SubgrupoService
        , private tipoService : TipoService
        , private messageService: MessageService
        , private confirmationService: ConfirmationService
        , private sanitizer: DomSanitizer)
    {

    }

    ngOnInit() {
        this.maxSize = environment.maxSize;

        this.cargarProductos();

        this.grupoService.getGrupos().subscribe(data => {
            this.grupos = data;
        });

        this.tipoService.getTipos().subscribe({
            next: (data) => {
                this.tipoProductos = data;
            },
            error: (error) => {
                console.log(error);
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al traer tipos de productos', life: 3000 });
            }
        });

        this.subgrupoService.getSubgrupos().subscribe(data => {
            this.subgruposTotal = data;
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

    cargarProductos() {
        this.productService.getProductos().subscribe(data => {
            data.forEach(item => {
                var foto = item.fotos !== undefined ? item.fotos[0] : null;
                if (foto) {
                    let objectURL = 'data:' + foto.tipo + ';base64,' + foto.archivo;
                    foto.imageUrl = this.sanitizer.bypassSecurityTrustUrl(objectURL);
                }
            });
            this.products = data
        });
    }

    openNew() {
        this.itemNew = new Producto();
        this.editando = false;
        this.submitted = false;
        this.productDialog = true;
    }

    deleteSelectedProducts() {
        this.deleteProductsDialog = true;
    }

    editProduct(product: IProducto) {
        this.editando = true;
        this.itemNew = { ...product };

        const subgrupo = this.subgruposTotal.find(x => x.id == product.subgrupo)

        const findGrupo = this.grupos.find(x => x.id == subgrupo?.grupoId);
        if (findGrupo) {
            this.selectedGrupo = findGrupo;
        }

        this.subgrupos = this.selectedGrupo !== null ? this.subgruposTotal.filter(x => x.grupoId == this.selectedGrupo.id) : [];

        const findSubgrupo = this.subgrupos.find(x => x.id == product.subgrupo);
        if (findSubgrupo) {
            this.selectedSubgrupo = findSubgrupo;
        }

        const findTipo = this.tipoProductos.find(x => x.id == product.tipo);
        if (findTipo) {
            this.selectedTipo = findTipo;
        }

        this.productService.getFotosProducto(product.id).subscribe({
            next: (data) => {
                this.itemNew.fotos = data;
            },
            error: (error) => {
                console.log(error);
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al traer fotos de producto', life: 3000 });
            }
        });
        
        this.productDialog = true;
    }

    deleteProduct(product: IProducto) {
        this.deleteProductDialog = true;
        this.itemNew = { ...product };
    }

    confirmDeleteSelected() {
        this.deleteProductsDialog = false;
        this.products = this.products.filter(val => !this.selectedProducts.includes(val));

        const ids = this.selectedProducts.map(producto => producto.id);

        from(ids).pipe(
            concatMap(id => this.productService.deleteProduct(id))
        ).subscribe({
            next: (data) => {
                console.log('Resultado:', data);
            },
            error:(error) => {
                console.error('Error:', error);
            },
            complete: () => {
                console.log('Todas las llamadas completadas');
                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Productos eliminados', life: 3000 });
            }
        });

        
        this.selectedProducts = [];
    }

    confirmDelete() {
        this.deleteProductDialog = false;
        const deleteId = this.itemNew.id;
        this.productService.deleteProduct(deleteId).subscribe({
            next: (data) => {
                this.products = this.products.filter(val => val.id != deleteId);
                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Producto eliminado', life: 3000 });
            },
            error: (error) => {
                console.log(error);
                this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al eliminar producto', life: 3000 });
            }
        });
        this.itemNew = new Producto();
    }

    hideDialog() {
        this.productDialog = false;
        this.submitted = false;
    }

    validarCampos(): boolean {
        if (this.itemNew.codigo == undefined || this.itemNew.codigo == "") {
            this.messageService.add({ severity: 'error', summary: 'Validaciones', detail: 'Debe ingresar un código', life: 3000 });
            return false;
        }
        if (this.itemNew.nombre == undefined || this.itemNew.nombre == "") {
            this.messageService.add({ severity: 'error', summary: 'Validaciones', detail: 'Debe ingresar un nombre', life: 3000 });
            return false;
        }
        if (this.itemNew.descripcion == undefined || this.itemNew.descripcion == "") {
            this.messageService.add({ severity: 'error', summary: 'Validaciones', detail: 'Debe ingresar una descripción', life: 3000 });
            return false;
        }
        if (this.itemNew.cantidad == undefined || this.itemNew.cantidad == null) {
            this.messageService.add({ severity: 'error', summary: 'Validaciones', detail: 'Debe ingresar una cantidad', life: 3000 });
            return false;
        }
        if (this.itemNew.precio == undefined || this.itemNew.precio == null) {
            this.messageService.add({ severity: 'error', summary: 'Validaciones', detail: 'Debe ingresar un precio', life: 3000 });
            return false;
        }
        if (this.selectedGrupo.codigo == undefined) {
            this.messageService.add({ severity: 'error', summary: 'Validaciones', detail: 'Debe seleccionar un grupo', life: 3000 });
            return false;
        }
        if (this.selectedSubgrupo.codigo == undefined) {
            this.messageService.add({ severity: 'error', summary: 'Validaciones', detail: 'Debe seleccionar un subgrupo', life: 3000 });
            return false;
        }
        if (this.selectedTipo.codigo == undefined) {
            this.messageService.add({ severity: 'error', summary: 'Validaciones', detail: 'Debe seleccionar un tipo de producto', life: 3000 });
            return false;
        }
        if (this.itemNew.fotos.length > 3) {
            this.messageService.add({ severity: 'error', summary: 'Validaciones', detail: 'Debe adjuntar máximo tres fotos', life: 3000 });
            return false;
        }
        if (this.itemNew.fotos.length == 0) {
            this.messageService.add({ severity: 'error', summary: 'Validaciones', detail: 'Debe adjuntar una foto', life: 3000 });
            return false;
        }
        return true;
    }


    save(): void {
        this.submitted = true;
        if (!this.validarCampos()) {
            return;
        }

        if (this.fotosSelect > 0) {
            this.confirmationService.confirm({
                message: '¿Está seguro de que desea guardar? Hay imágenes pendientes por cargar.',
                header: 'Confirmación',
                icon: 'pi pi-exclamation-triangle',
                accept: () => {
                    this.acceptSave();
                }, reject: () => {
                    this.messageService.add({ severity: 'info', summary: 'Cancelado', detail: 'Cancelada la operación.', life: 3000 });
                }
            });
        } else {
            this.acceptSave();
        }



    }

    acceptSave() {
        this.submitted = true;

        this.itemNew.subgrupo = this.selectedSubgrupo.id;
        this.itemNew.tipo = this.selectedTipo.id;

        //Guardar
        if (this.editando) {

            this.productService.updateProduct(this.itemNew).subscribe({
                next: (data) => {
                    this.cargarProductos();
                    this.productDialog = false;
                    this.messageService.add({ severity: 'success', summary: 'Correcto', detail: 'Producto actualizado.', life: 3000 });
                }
                , error: (error) => {
                    console.log(error);
                    this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al actualizar producto.', life: 3000 });
                }
            });
        } else {
            this.productService.createProduct(this.itemNew).subscribe({
                next: (data) => {
                    this.cargarProductos();
                    this.productDialog = false;
                    this.messageService.add({ severity: 'success', summary: 'Correcto', detail: 'Producto registrado.', life: 3000 });
                }
                , error: (error) => {
                    console.log(error);
                    this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error al guardar producto.', life: 3000 });
                }
            });
        }
    }

    findIndexById(id: number): number {
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

    cambioGrupo(event: any) {
        this.subgrupos = this.selectedGrupo !== null?this.subgruposTotal.filter(x => x.grupoId == this.selectedGrupo.id):[];
    }

    eliminarArchivo(archivo: Foto) {
        const index = this.itemNew?.fotos?.findIndex(x => x.id == archivo.id);
        if (index !== undefined) {
            this.itemNew?.fotos?.splice(index, 1);
        }
    }

    descargarArchivo(archivo: Foto) {
        const link = document.createElement('a');
        link.href = 'data:' + archivo.tipo + ';base64,' + archivo.archivo;
        link.download = archivo.nombre;
        link.click();
    }

    moverArriba(index: number) {
        if (index > 0) {
            const temp = this.itemNew.fotos[index];
            this.itemNew.fotos[index] = this.itemNew.fotos[index - 1];
            this.itemNew.fotos[index - 1] = temp;
        }
    }

    // Mueve el archivo una posición hacia abajo
    moverAbajo(index: number) {
        if (index < this.itemNew.fotos.length - 1) {
            const temp = this.itemNew.fotos[index];
            this.itemNew.fotos[index] = this.itemNew.fotos[index + 1];
            this.itemNew.fotos[index + 1] = temp;
        }
    }

    get currentImage(): SafeUrl {
        return this.currentFoto.imageUrl;
    }

    mostrarArchivo(archivo: Foto) {
        this.displayFotos = true;
        let objectURL = 'data:' + archivo.tipo + ';base64,' + archivo.archivo;
        this.currentFoto.imageUrl = this.sanitizer.bypassSecurityTrustUrl(objectURL);
    }

    ocultarArchivo() {
        this.displayFotos = false;
    }
}
