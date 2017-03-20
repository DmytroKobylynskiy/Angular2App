import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UniversalModule } from 'angular2-universal';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { HelloWorldComponent } from './components/helloworld/helloworld.component';
import { WeatherComponent } from './components/weather/weather.component';
import { TaxiOrdersComponent } from './components/taxiorders/taxiorders.component';
import { ChangeRoleComponent } from './components/changerole/change-role.component';
import {CreateOrderComponent} from './components/taxiorders/createorder.component';
import { HttpModule, Http, RequestOptions } from '@angular/http';
import { provideAuth, AuthHttp, AuthConfig,AuthModule, AuthConfigConsts } from 'angular2-jwt';
import { Ng2MapModule} from 'ng2-map';
export function authHttpServiceFactory(http: Http, options: RequestOptions) {
  return new AuthHttp( new AuthConfig({}), http, options);
}

@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        HelloWorldComponent,
        WeatherComponent,
        TaxiOrdersComponent,
        ChangeRoleComponent,
        CreateOrderComponent
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        FormsModule,
        HttpModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'hello', component: HelloWorldComponent },
            { path: 'weather', component: WeatherComponent },
            { path: 'createTaxiOrder', component : CreateOrderComponent},
            { path: 'changeRole', component : ChangeRoleComponent},
            { path: '**', redirectTo: 'home' }
        ]),
        Ng2MapModule.forRoot({apiUrl: 'https://maps.google.com/maps/api/js?sensor=true&libraries=places&key=AIzaSyBaiH4wZdZ3nlL9itqn6-D7-LOUsdGuyD4'})
        
    ],
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
    })
  ]
    
})
export class AppModule {
}
