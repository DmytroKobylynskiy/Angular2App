import { Component } from '@angular/core';
import { Auth1Service } from '../services/auth1.service';
import { HttpService} from '../services/http.service';
import {Http,Response, Headers, URLSearchParams} from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import '../entities/mes';
@Component({
    selector: 'nav-menu',
    template: require('./navmenu.component.html'),
    styles: [require('./navmenu.component.css')],
    providers: [HttpService,Auth1Service]
})
export class NavMenuComponent {

    private userRole : string;
    private nsLength : number;
    public loggedIn : boolean;
    public done : boolean;
    public notifications : Array<NotificationOrder>; 
    constructor(private http: Http,private httpService: HttpService,private authService: Auth1Service) {
    }

    ngOnInit(){
        this.getNumNotifications();
    }

    login(){
        this.authService.login();
    }
    getNumNotifications(){
        if(this.authService.loggedIn()){
            let params: URLSearchParams = new URLSearchParams();
            params.set('ownerId', this.authService.userProfile.user_id);
            this.http.get('api/notification/getNumNotificationsOrder', {
                search: params
            }).subscribe(
            (response) => {this.nsLength = response.json();this.done=true}, 
            (error) => console.log("error")
        ); 
        }
    }

    getNotifications(){
        if(this.authService.loggedIn()){
            let params: URLSearchParams = new URLSearchParams();
            params.set('ownerId', this.authService.userProfile.user_id);
            this.http.get('api/notification/getNotificationsOrder', {
                search: params
            }).subscribe(
            (response) => {this.notifications = response.json();this.nsLength=this.notifications.length;this.done=true}, 
            (error) => console.log("error")
        ); 
        }
    }

}
