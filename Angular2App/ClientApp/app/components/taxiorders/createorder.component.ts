import { Component, ChangeDetectorRef } from '@angular/core';
import { Http } from '@angular/http';
import { HttpService} from './http.service';
import { NgForm} from '@angular/forms';
import {Response, Headers, URLSearchParams} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import './taxiorders';

@Component({
    selector: 'angular2app',
    template: require('./createorder.component.html'),
    providers: [HttpService]
})

export class CreateOrderComponent {
    public taxiOrders: Array<TaxiOrder>;
    public taxiOrder : TaxiOrder;
    public str : string;
    public done : boolean ;
    public condition: boolean=false;
    constructor(private http: Http,private httpService: HttpService,private ref: ChangeDetectorRef) {
        
    }
    createTaxiOrder(form : NgForm){
        const body = JSON.stringify(this.taxiOrder);
        console.log(form.value.startPoint);
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        this.condition = true;
        this.httpService.postData(form)
                .subscribe((data) => {this.str=data; this.done=true;});
        
    }
    public getTaxiOrder(chosenCity: string) {
        this.http.get('/api/order/taxiorders').subscribe(result => {
            this.taxiOrders = result.json();
        });
    }

    submit(form: NgForm){
        console.log(form.value.startPoint);
    }

        public positions = [  ];
    onMapReady(map) {
        console.log('map', map);
        console.log('markers', map.markers);  // to get all markers as an array 
    }
    onIdle(event) {
        console.log('map', event.target);
    }
    onMarkerInit(marker) {
        console.log('marker', marker);
    }
    onMapClick(event) {
        this.positions.push(event.latLng);
        event.target.panTo(event.latLng);
    }
    showRandomMarkers() {
        let randomLat: number, randomLng: number;
        this.positions = [];
        for (let i = 0 ; i < 9; i++) {
        randomLat = Math.random() * 0.0099 + 43.7250;
        randomLng = Math.random() * 0.0099 + -79.7699;
        this.positions.push([randomLat, randomLng]);
        }
    }
    addMarker() {
        let randomLat = Math.random() * 0.0099 + 43.7250;
        let randomLng = Math.random() * 0.0099 + -79.7699;
        this.positions.push([randomLat, randomLng]);
    }


    

    nav = null;
    requestPosition() {
        if (this.nav == null) {
            this.nav = window.navigator;
        }
        if (this.nav != null) {
            var geoloc = this.nav.geolocation;
            if (geoloc != null) {
                geoloc.getCurrentPosition(this.successCallback);
            }
            else {
                alert("geolocation not supported");
            }
        }
        else {
            alert("Navigator not found");
        }
    }
    successCallback(position) {
       console.log(position);
    }
    

    autocomplete: any;
    address: any = {};
    center: any;

    initialized(autocomplete: any) {
        console.log("2132123123");
        this.autocomplete = autocomplete;
    }
    placeChanged(place) {
        this.center = place.geometry.location;
        for (let i = 0; i < place.address_components.length; i++) {
        let addressType = place.address_components[i].types[0];
        this.address[addressType] = place.address_components[i].long_name;
        }
        this.ref.detectChanges();
    }
}

