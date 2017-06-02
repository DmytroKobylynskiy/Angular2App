import { Component, ChangeDetectorRef } from '@angular/core';
import { Http } from '@angular/http';
import { HttpService} from '../services/http.service';
import { MapsService} from '../services/maps.service';
import { NgForm} from '@angular/forms';
import {Response, Headers, URLSearchParams} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import './taxioffer';
import { Auth1Service } from "../services/auth1.service";
import { Router } from "@angular/router";
import { OffersService } from "../services/offers.service";

@Component({
    selector: 'angular2app',
    template: require('./createoffer.component.html'),
    providers: [HttpService,MapsService,Auth1Service,OffersService]
})

export class CreateOfferComponent {
    public offers: Array<TaxiOffer>;
    public taxiOffer : TaxiOffer;
    public start : string;
    public end : string;
    public str : string;
    public strStr : string;
    public strEnd : string;
    public done : boolean ;
    public condition: boolean=false;
    constructor(private router: Router,private auth:Auth1Service,private http: Http,private httpService: HttpService,private mapsService: MapsService,private ref: ChangeDetectorRef) {
        
    }
    createOffer(form : NgForm){
        if(this.auth.loggedIn()){
            let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
            this.condition = true;
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
                form.value.place = this.mapsService.getPos() + "|" + this.start;
                form.value.offerOwnerId = this.auth.userProfile.user_id;
                form.value.offerOwnerEmail = this.auth.userProfile.email;
                console.log(form.value);
                this.httpService.postOffer(form)
                    .subscribe((data) => {this.router.navigate(['offers']);this.done=true;});
            });
        }else{
            this.router.navigate(['home']);
        }  
    }
    public getOffers(chosenCity: string) {
        this.http.get('/api/offer/taxioffers').subscribe(result => {
            this.offers = result.json();
        });
    }

    submit(form: NgForm){
        console.log(form.value.startPoint);
    }
}

