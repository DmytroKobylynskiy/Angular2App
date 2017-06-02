import { Component } from '@angular/core';
import { Auth1Service } from '../services/auth1.service';
import { HttpService} from '../services/http.service';
import {Http,Response, Headers, URLSearchParams} from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { NotificationService } from "../services/notification.service";
@Component({
    selector: 'home',
    template: require('./home.component.html'),
    styles: [require('./home.component.css')],
    providers: [HttpService,Auth1Service,NotificationService]
})
export class HomeComponent {

    private userRole : string;
    private nsLength : number;
    public loggedIn : boolean;
    public done : boolean = false;
    public name : string;
    constructor(private notificationService: NotificationService,private http: Http,private httpService: HttpService,private authService: Auth1Service) {
        
            
    }


    ngOnInit(){
      if(this.authService.loggedIn()){
            this.name = this.authService.userProfile.name;
            this.done = true;
        }
       
    }

    getNumNotifications(){
        this.notificationService.getNumNotifications(this.authService.userProfile.user_id);
        console.log(this.notificationService.nsLength);
        this.nsLength = this.notificationService.nsLength;
    }

    login(){
        this.authService.login();
    }
}
