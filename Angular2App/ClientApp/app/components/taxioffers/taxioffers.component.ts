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


@Component({
    selector: 'angular2app',
    template: require('./taxioffers.component.html'),
    styles: [require('./taxioffers.component.css')],
    providers: [HttpService,Auth1Service]
})

export class TaxiOffersComponent {
    public taxiOffers: Observable<Array<TaxiOffer>>;
    public taxiOffersId: Observable<Array<TaxiOffer>>;
    public taxiOffer : TaxiOffer;
    public str : string;
    public done : boolean ;
    public condition: boolean=false;
    public condition2: boolean=false;
    private isAdmin : boolean = false;
    private isCarrier : boolean = false;
    private isUser : boolean = false;
    private selectedId: number;
    constructor(private http: Http,private httpService: HttpService,private route: ActivatedRoute,
        private router: Router,private authService: Auth1Service,overlay: Overlay, vcRef: ViewContainerRef, public modal: Modal) {
    overlay.defaultViewContainer = vcRef;
  }
    ngOnInit() {
        if(this.authService.loggedIn()){
            this.http.get('/api/offer/offers').subscribe(result => {
                this.taxiOffers = result.json();
            });
            this.taxiOffersId = this.route.params.switchMap((params: Params) => {
                this.selectedId = +params['id'];
                console.log("ddsd"+this.selectedId);
                return this.taxiOffers;
            });
            switch(this.authService.getUserProfile().user_metadata.role){
                case "carrier" : this.isCarrier=true; break;
                case "customer" : this.isUser=true; break;
                case "admin" : this.isAdmin = true; break;
            }
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
        console.log(item.offerOwnerId);
        this.router.navigate(['createTaxiOrder', item.offerOwnerId]);
    }
    
}


