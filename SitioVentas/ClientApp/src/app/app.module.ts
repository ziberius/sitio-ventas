import { NgModule } from '@angular/core';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AppLayoutModule } from './layout/app.layout.module';
import { NotfoundComponent } from './demo/components/notfound/notfound.component';
import { ProductService } from './demo/service/product.service';
import { CountryService } from './demo/service/country.service';
import { CustomerService } from './demo/service/customer.service';
import { EventService } from './demo/service/event.service';
import { IconService } from './demo/service/icon.service';
import { NodeService } from './demo/service/node.service';
import { PhotoService } from './demo/service/photo.service';
import { ConvertirStockDirective } from './demo/directives/convertir-stock.directive';
import { MenubarModule } from 'primeng/menubar';
import { GrupoService } from './demo/service/grupo.service';
import { SubgrupoService } from './demo/service/subgrupo.service';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api';
import { TipoService } from './demo/service/tipo.service';


@NgModule({
    declarations: [
        AppComponent, NotfoundComponent, ConvertirStockDirective
    ],
    imports: [
        AppRoutingModule,
        AppLayoutModule,
        ConfirmDialogModule,
        MenubarModule
    ],
    providers: [
        { provide: LocationStrategy, useClass: HashLocationStrategy },
        CountryService, CustomerService, EventService, IconService, NodeService,
        PhotoService, ProductService, GrupoService, SubgrupoService, ConfirmationService
        , TipoService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
