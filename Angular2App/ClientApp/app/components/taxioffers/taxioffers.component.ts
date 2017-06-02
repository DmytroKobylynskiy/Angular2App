import { Http } from '@angular/http';
import { HttpService} from '../services/http.service';
import { NgForm} from '@angular/forms';
import {Response, Headers, URLSearchParams} from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/catch';
import './taxioffer';
import { Auth1Service } from "../services/auth1.service";
import { Component, ViewContainerRef } from '@angular/core';
import { Overlay } from 'angular2-modal';
import { Modal } from 'angular2-modal/plugins/bootstrap';
import { MapsService } from "../services/maps.service";
import { OffersService } from "../services/offers.service";


@Component({
    selector: 'angular2app',
    template: require('./taxioffers.component.html'),
    styles: [require('./taxioffers.component.css')],
    providers: [MapsService,HttpService,Auth1Service,OffersService]
})

export class TaxiOffersComponent {
    public taxiOffers: Observable<Array<TaxiOffer>>;
    public taxiOffersCoord: Array<TaxiOffer>;
    public taxiOffersId: Observable<Array<TaxiOffer>>;
    public taxiOffer : TaxiOffer;
    public order : string;
    reverse: boolean = false;
    public str : string;
    public done : boolean ;
    public map : boolean = false;
    public condition: boolean=false;
    public condition2: boolean=false;
    private isAdmin : boolean = false;
    private isCarrier : boolean = false;
    private isUser : boolean = false;
    private clickedOnMarker : boolean = false;
    private selectedId: number;
    constructor(
        private offersService:OffersService,
        private mapsService:MapsService,
        private http: Http,
        private httpService: HttpService,
        private route: ActivatedRoute,
        private router: Router,
        private authService: Auth1Service) {
    }
    ngOnInit() {
        if(this.authService.loggedIn()){
            this.offersService.offers().subscribe((result)=>{this.taxiOffers=this.offersService.taxiOffers;});
            
            this.taxiOffersId = this.route.params.switchMap((params: Params) => {
                this.selectedId = +params['id'];
                return this.taxiOffers;
            });
            switch(this.authService.getUserProfile().user_metadata.role){
                case "carrier" : this.isCarrier=true; break;
                case "customer" : this.isUser=true; break;
                case "admin" : this.isAdmin = true; break;
            }
            this.offersService.offersCoord().subscribe((result)=>
                {   this.taxiOffersCoord=this.offersService.taxiOffersCoord;
                    for (var index = 0; index < this.taxiOffersCoord.length; index++) {
                        this.mapsService.positions[index]= this.taxiOffersCoord[index].place;
                    }
                }
            );
            
       }else{
            this.router.navigate(['home']);
        }   
    }

    public onSelect(item) {
        console.log(item);
        console.log(this.taxiOffers[item]);
        this.taxiOffers[item].condition = !this.taxiOffers[item].condition;
    }

    public onEdit(item) {
        console.log(item.id);
        this.router.navigate(['offers', item.id]);
    }

    public onDelete(item) {
        let params: URLSearchParams = new URLSearchParams();
        params.set('index', item.id);
        console.log(item.id);
        this.http.get('api/offer/removeTaxiOffer', {
                search: params
            }).subscribe(
            (response) => {this.taxiOffers = response.json();this.done=true}, 
            (error) => console.log("error")
        );
    }

    public onAgree(item){
        console.log(item);
        this.router.navigate(['createTaxiOrder', item.id]);
    }

    public showList(){
        this.map=false;
    }
    public showMap(){
        this.map=true;
    }

    public onAgreeByMap(place){
        console.log("dssd"+place);
    }

    clicked(event,position) {
        this.clickedOnMarker = true;
        let marker = event.target;
        //console.log(event);
        /*var lat = marker.getPosition().lat().toString().substring(0,marker.getPosition().lat().toString().indexOf('.')+3);
        var lng = marker.getPosition().lng().toString().substring(0,marker.getPosition().lng().toString().indexOf('.')+3);
        console.log(this.positions);
        console.log(lat+','+lng);*/

        for (var index = 0; index < this.taxiOffersCoord.length; index++) {
            //console.log(this.taxiOffers[index].place);
            if(this.taxiOffersCoord[index].place==position){
                //console.log(this.taxiOffers[index]);
                this.taxiOffer = this.taxiOffers[index];
                //console.log(position);
                this.mapsService.clicked(event,this.taxiOffer);
            }
        }
        //console.log(offer);
        /*marker.ng2MapComponent.openInfoWindow('iw', marker, {
            price: offer.price, place: offer.place,
        });*/
    }

    setOrder(value: string) {
        if (this.order === value) {
            this.reverse = !this.reverse;
        }
        this.order = value;
    }

}


