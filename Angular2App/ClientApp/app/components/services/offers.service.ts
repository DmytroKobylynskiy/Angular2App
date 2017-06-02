import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {Http,Response, Headers, URLSearchParams} from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Injectable()
export class OffersService{
    taxiOffer: any;
    done: boolean;
    nsLength : number;
    public taxiOffers: Observable<Array<TaxiOffer>>;
    public taxiOffersCoord: Array<TaxiOffer>;
    constructor(private http:Http){
        
    }
    public onDelete(item) {
        let params: URLSearchParams = new URLSearchParams();
        params.set('offerId', item.id);
        console.log(item.id);
        this.http.get('api/offer/removeTaxiOffer', {
                search: params
            }).subscribe(
            (response) => {this.taxiOffers = response.json();this.done=true}, 
            (error) => console.log("error")
        );
    }

    offers(){
        return this.http.get('/api/offer/offers').map(response => {this.taxiOffers = response.json() })
                        .catch((error:any) =>{return Observable.throw(error);});
    }

    offersCoord(){
        return this.http.get('/api/offer/offersCoord').map(response => {this.taxiOffersCoord = response.json()  })
                        .catch((error:any) =>{return Observable.throw(error);});
    }
    offerByLatLng(position){
        for (var index = 0; index < this.taxiOffersCoord.length; index++) {
            console.log(this.taxiOffers[index].place);
            if(this.taxiOffers[index].place==position){
                console.log(this.taxiOffers[index]);
                return this.taxiOffers[index];
            }
                
        }
    }

    offerById(id){
        let params: URLSearchParams = new URLSearchParams();
        params.set('index', id);
        return this.http.get('/api/offer/offerById', {
                search: params
            }).map(response => {this.taxiOffer = response.json()  })
                        .catch((error:any) =>{return Observable.throw(error);});
    }
}