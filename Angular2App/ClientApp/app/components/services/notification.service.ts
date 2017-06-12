import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {Http,Response, Headers, URLSearchParams} from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Auth1Service } from "./auth1.service";

@Injectable()
export class NotificationService{
    done : boolean;
    nsLength : number;
    role : any;
    notifications : Array<Notification>;

    constructor(private authService :Auth1Service,private http:Http){
        //this.role= this.authService.getUserProfile().user_metadata.role;
    }
    getNumNotifications(user_id:string){
            let params: URLSearchParams = new URLSearchParams();
            params.set('ownerId', user_id);
            params.set('role',this.authService.getUserProfile().user_metadata.role);
            return this.http.get('api/notification/getNumNotificationsOrder', {
                                search: params
                            }).map((response:Response) =>  {response.json();this.nsLength = response.json();})
                            .catch((error:any) =>{return Observable.throw(error);});
        
    }

    getNotifications=(user_id)=>{
        
            let params: URLSearchParams = new URLSearchParams();
            params.set('ownerId', user_id);
            params.set('role',this.authService.getUserProfile().user_metadata.role);
            this.http.get('api/notification/getNotificationsOrder', {
                search: params
            }).subscribe(
            (response) => {this.notifications = response.json();this.nsLength=this.notifications.length;this.done=true;}, 
            (error) => console.log("error")
        ); 
        
    }
}