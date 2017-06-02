import { Component } from '@angular/core';
import { Auth1Service } from '../services/auth1.service';
import { HttpService} from '../services/http.service';
import {Http,Response, Headers, URLSearchParams} from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import '../entities/mes';
import { NotificationService } from "../services/notification.service";
@Component({
    selector: 'nav-menu',
    template: require('./navmenu.component.html'),
    styles: [require('./navmenu.component.css')],
    providers: [HttpService,Auth1Service,NotificationService]
})
export class NavMenuComponent {

    private userRole : string;
    private nsLength : number;
    public loggedIn : boolean;
    public done : boolean;
    public notifications : Array<NotificationOrder>; 
    constructor(private notificationService: NotificationService,private http: Http,private httpService: HttpService,private authService: Auth1Service) {
        if(this.authService.loggedIn())
         this.notificationService.getNumNotifications(this.authService.userProfile.user_id).subscribe((data) => {this.nsLength=data;}
         );
    }


    ngOnInit(){
        
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
