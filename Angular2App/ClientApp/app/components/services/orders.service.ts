import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {Http,Response, Headers, URLSearchParams} from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Injectable()
export class OrdersService{
    done : boolean;
    nsLength : number;
    taxiOrders : Observable<Array<TaxiOrder>>;
    taxiOrdersId: Observable<Array<TaxiOrder>>;
    private selectedId: number;
    constructor(private router: Router,private http:Http,private route: ActivatedRoute){
        
    }

    public orders(){
        this.http.get('/api/order/taxiorders').subscribe(result => {
                this.taxiOrders = result.json();
            });
        this.taxiOrdersId = this.route.params.switchMap((params: Params) => {
                this.selectedId = +params['id'];
                console.log("ddsd"+this.selectedId);
                return this.taxiOrders;
            });
    }

    

    public onDelete(item) {
        let params: URLSearchParams = new URLSearchParams();
        params.set('index', item.id);
        console.log(item.id);
        this.http.get('api/order/removetaxiorder', {
                search: params
            }).subscribe(
            (response) => {this.taxiOrders = response.json();this.done=true}, 
            (error) => console.log("error")
        );
    }

    getNumNotifications(user_id:string){
        
            let params: URLSearchParams = new URLSearchParams();
            params.set('ownerId', user_id);
            return this.http.get('api/notification/getNumNotificationsOrder', {
                                search: params
                            }).map((response:Response) =>  {response.json();this.nsLength = response.json();})
                            .catch((error:any) =>{return Observable.throw(error);});
        
    }
}