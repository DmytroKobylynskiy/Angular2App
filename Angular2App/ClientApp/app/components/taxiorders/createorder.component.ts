import { Component, ChangeDetectorRef, ViewChild } from '@angular/core';
import { Http } from '@angular/http';
import { HttpService} from '../services/http.service';
import { MapsService} from '../services/maps.service';
import { NgForm} from '@angular/forms';
import {Response, Headers, URLSearchParams} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import './taxiorders';
import { Auth1Service } from "../services/auth1.service";
import { OffersService } from "../services/offers.service";

@Component({
    selector: 'angular2app',
    template: require('./createorder.component.html'),
    providers: [HttpService,MapsService,Auth1Service,OffersService]
})

export class CreateOrderComponent {
    public taxiOrders: Array<TaxiOrder>;
    public taxiOrder : TaxiOrder;
    public start : string;
    public end : string;
    public str : string;
    public date : string;
    public time : string;
    public name : string;
    public strStr : string;
    public strEnd : string;
    public done : boolean ;
    public condition: boolean=false;
    constructor(private auth : Auth1Service,
                private http: Http,
                private httpService: HttpService,
                private mapsService: MapsService,
                private cdr: ChangeDetectorRef) {
    }
    createTaxiOrder(form : NgForm){
        const body = JSON.stringify(this.taxiOrder);
        //

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        this.condition = true;
        console.log(this.mapsService.positions[0][1]);
        this.strStr = "https://maps.googleapis.com/maps/api/geocode/json?latlng="+this.mapsService.positions[0][0] +","+  this.mapsService.positions[0][1]+"&sensor=true";
        this.http.get(this.strStr).map(res => {
            if(res.status != 200) {
                throw new Error('Error ' + res.status);
            } else {
                return res.json();
            }
        })
        .subscribe(res => {
            this.start = res.results[0].formatted_address;
            form.value.startPoint = this.mapsService.getPos() + "|" + this.start;
            this.strEnd = "https://maps.googleapis.com/maps/api/geocode/json?latlng="+this.mapsService.end_positions[0][0] +","+  this.mapsService.end_positions[0][1]+"&sensor=true";
            this.http.get(this.strEnd).map(res => {
                if(res.status != 200) {
                    throw new Error('Error ' + res.status);
                } else {
                    return res.json();
                }
            }).subscribe(res => {
                this.end = res.results[0].formatted_address;
                form.value.endPoint = this.mapsService.getEndPos() + "|" + this.end;
                console.log(form.value.startPoint +" " + form.value.endPoint); 
                form.value.orderOwnerId=this.auth.userProfile.user_id;
                form.value.orderOwnerEmail=this.auth.userProfile.email;
                form.value.date = this.date;
                form.value.time = this.time;
                console.log(form.value);
                this.httpService.postData(form)
                    .subscribe((data) => {this.str=data; this.done=true;});
            });
        });   
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

