import { Component, ChangeDetectorRef,AfterViewInit } from '@angular/core';
import { Http } from '@angular/http';
import { HttpService} from '../services/http.service';
import { MapsService} from '../services/maps.service';
import { NgForm} from '@angular/forms';
import {Response, Headers, URLSearchParams} from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import './taxioffer';
import { OffersService } from "../services/offers.service";

@Component({
    selector: 'angular2app',
    template: require('./editoffer.component.html'),
    providers: [HttpService,MapsService,OffersService]
})

export class EditOfferComponent {
    public taxiOffers: Array<TaxiOffer>;
    public taxiOffer : TaxiOffer;
    private id : string;
    public start : string;
    public end : string;
    public str : string;
    public strStr : string;
    public strEnd : string;
    public done : boolean ;
    public condition: boolean=false;
    constructor(private route: ActivatedRoute,private router: Router,private http: Http,private httpService: HttpService,private mapsService: MapsService,private ref: ChangeDetectorRef) {
       
    }
    ngOnInit() {
        console.log(this.route.snapshot.url[1].path);
        this.id = this.route.snapshot.url[1].path;
        let params: URLSearchParams = new URLSearchParams();
        params.set('id', this.route.snapshot.url[1].path);
        this.http.get('api/offer/edittaxioffer', {
                search: params
            }).subscribe(
            (response) => {this.taxiOffer = response.json();this.done=true;console.log(this.taxiOffer)}, 
            (error) => console.log("error")
        );
        
    }

    get(){
        console.log(this.taxiOffer);
    }
    public EditOffer(form : NgForm){
        const body = JSON.stringify(this.taxiOffer);
        //

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
            console.log(form.value.place);
            this.httpService.postOffer(form)
                .subscribe((data) => {this.str=data; this.done=true;});
        });
              
    }

    submit(form: NgForm){
        console.log(form.value.startPoint);
    }
}

