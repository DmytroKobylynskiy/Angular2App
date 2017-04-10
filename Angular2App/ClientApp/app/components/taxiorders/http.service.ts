import {Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {Response, Headers} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
 import { NgForm} from '@angular/forms';
@Injectable()
export class HttpService{
 
    constructor(private http: Http){ }

    
    postData(obj: NgForm){
        const body = JSON.stringify(obj.value);
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        return this.http.post('api/order/createTaxiOrder', body, { headers: headers })
                        .map((resp:Response)=>resp.json())
                        .catch((error:any) =>{return Observable.throw(error);}); 
    }

    updateData(obj: NgForm){
        const body = JSON.stringify(obj.value);
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' }); 
        return this.http.post('api/order/editTaxiOrder', body, { headers: headers })
                        .map((resp:Response)=>resp.json())
                        .catch((error:any) =>{return Observable.throw(error);}); 
    }
    /*
    getData(){
        return this.http.post('api/order/editTaxiOrder', body, { headers: headers })
                        .map((resp:Response)=>resp.json())
                        .catch((error:any) =>{return Observable.throw(error);}); 
    }*/
}
