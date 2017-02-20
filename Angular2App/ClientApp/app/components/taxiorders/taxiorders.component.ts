import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { NgForm} from '@angular/forms';
import {Response, Headers, URLSearchParams} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/catch';

@Component({
    selector: 'angular2app',
    template: require('./taxiorders.component.html')
})

export class TaxiOrdersComponent {
    public taxiOrders: Array<TaxiOrder>;

    constructor(private http: Http) {
    }
    createTaxiOrder(taxiOrder : TaxiOrder){
        const body = JSON.stringify(taxiOrder);
         
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
         
        return this.http.post('http://localhost:5000/api/order/createTaxiOrder', body, { headers: headers })
                        .map((resp:Response)=>resp.json())
                        .catch((error:any) =>{return Observable.throw(error);}); 
        
    }
    public getTaxiOrder(chosenCity: string) {
        this.http.get('/api/order/taxiorders').subscribe(result => {
            this.taxiOrders = result.json();
        });
    }

    submit(form: NgForm){
        console.log(form.value.startPoint);
    }
}

interface TaxiOrder {
    taxiOrderId : string;
    startPoint : string;
    endPoint : string;
    date : string;
    time : string;
    withAnimals : boolean;
    freightCar : boolean;
    distanse : number;
    duration : number;
    passengerPhone : string ;
    PassengerName : string;
    OrderOwnerId : string;
    OrderStatus : string;
    ReceiverId : string;    
}
