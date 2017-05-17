import { NgModule } from '@angular/core';
import { RouterModule,Routes,Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UniversalModule } from 'angular2-universal';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { HelloWorldComponent } from './components/helloworld/helloworld.component';
import { TaxiOrdersComponent } from './components/taxiorders/taxiorders.component';
import { ChangeRoleComponent } from './components/changerole/change-role.component';
import { CreateOrderComponent} from './components/taxiorders/createorder.component';
import { CreateOfferComponent} from './components/taxioffers/createoffer.component';
import { TaxiOffersComponent} from './components/taxioffers/taxioffers.component';
import { EditOrderComponent }  from './components/taxiorders/editorder.component';
import { EditOfferComponent }  from './components/taxioffers/editoffer.component';
import { HttpModule, Http, RequestOptions } from '@angular/http';
import { provideAuth, AuthHttp, AuthConfig,AuthModule, AuthConfigConsts } from 'angular2-jwt';
import { CommonModule } from '@angular/common';  
import { BrowserModule } from '@angular/platform-browser';
import { Ng2MapModule } from 'ng2-map';
import { MapsService } from "./components/services/maps.service";
import { ModalModule } from 'angular2-modal';
import { BootstrapModalModule } from "angular2-modal/plugins/bootstrap";
import { AdditionCalculateWindow } from "./components/taxiorders/modal";
import { CreateOrderToDriverComponent } from "./components/taxiorders/createorderToDriver.component";
import { NotificationsComponent } from "./components/notifications/notifications.component";
import { RequestsComponent } from "./components/changerole/requests.component";
export function authHttpServiceFactory(http: Http, options: RequestOptions) {
  return new AuthHttp( new AuthConfig({}), http, options);
}
const taxiOrderRoutes: Routes = [
  { path: 'taxiorders',  component: TaxiOrdersComponent },
  { path: 'taxiorders/:id', component: EditOrderComponent },
  { path: 'createTaxiOrder/:receiverId', component: CreateOrderToDriverComponent }
];

const taxiOfferRoutes: Routes = [
  { path: 'offers',  component: TaxiOffersComponent },
  { path: 'offers/:id', component: EditOfferComponent },
];
@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        HomeComponent,
        HelloWorldComponent,
        TaxiOrdersComponent,
        ChangeRoleComponent,
        CreateOrderComponent,
        CreateOfferComponent,
        TaxiOffersComponent,
        EditOrderComponent,
        EditOfferComponent,
        NotificationsComponent,
        CreateOrderToDriverComponent,
        RequestsComponent,
        AdditionCalculateWindow
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        FormsModule,
        HttpModule,
        CommonModule,
        BrowserModule,
        ModalModule.forRoot(),
        BootstrapModalModule,
        RouterModule.forChild(taxiOrderRoutes),
        RouterModule.forChild(taxiOfferRoutes),
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'hello', component: HelloWorldComponent },
            { path: 'createTaxiOrder', component : CreateOrderComponent},
            { path: 'createOffer', component : CreateOfferComponent},
            { path: 'changeRole', component : ChangeRoleComponent},
            { path: 'requests', component : RequestsComponent},
            { path: 'offers', component : TaxiOffersComponent},
            { path: 'notifications', component : NotificationsComponent},
            { path: '**', redirectTo: 'home' }
        ]),
        Ng2MapModule.forRoot({apiUrl: 'https://maps.google.com/maps/api/js?sensor=true&libraries=places&key=AIzaSyBaiH4wZdZ3nlL9itqn6-D7-LOUsdGuyD4'})
        
    ],
    entryComponents: [ AdditionCalculateWindow ],
    providers: [
    {
      provide: AuthHttp,
      useFactory: authHttpServiceFactory,
      deps: [ Http, RequestOptions ]
    },
    
    
    provideAuth({
            headerName: 'Authorization',
            headerPrefix: 'bearer',
            tokenName: 'token',
            tokenGetter: (() => localStorage.getItem('id_token')),
            globalHeaders: [{ 'Content-Type': 'application/json' }],
            noJwtError: true
    }),MapsService
  ]
    
})
export class AppModule {
}
