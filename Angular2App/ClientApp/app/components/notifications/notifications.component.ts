import { Http } from '@angular/http';
import { HttpService} from '../services/http.service';
import { NgForm} from '@angular/forms';
import {Response, Headers, URLSearchParams} from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/catch';
import { Auth1Service } from "../services/auth1.service";
import { Component, ViewContainerRef } from '@angular/core';
import { Overlay } from 'angular2-modal';
import { Modal } from 'angular2-modal/plugins/bootstrap';
import '../entities/mes';
@Component({
    selector: 'angular2app',
    template: require('./notifications.component.html'),
    styles: [require('./notifications.component.css')],
    providers: [HttpService,Auth1Service]
})

export class NotificationsComponent {
    public notifications: Observable<Array<NotificationOrder>>;
    public notification : NotificationOrder;
    public taxiOffer : TaxiOffer;
    public isCarrier : boolean;
    public isUser : boolean;
    public str : string;
    public done : boolean ;
    private selectedId: number;
    private price;
    constructor(private http: Http,private httpService: HttpService,private route: ActivatedRoute,
        private router: Router,private authService: Auth1Service,overlay: Overlay, vcRef: ViewContainerRef, public modal: Modal) {
        overlay.defaultViewContainer = vcRef;
    }
    ngOnInit() {
        if(this.authService.loggedIn()){
            if(this.authService.getUserProfile().user_metadata.role == "carrier"){
                this.isCarrier = true;
                let params: URLSearchParams = new URLSearchParams();
                params.set('ownerId', this.authService.userProfile.user_id);
                this.http.get('api/notification/getnotificationsorder', {
                    search: params
                }).subscribe(
                (response) => {this.notifications=response.json();this.done=true}, 
                (error) => console.log("error")
            );
            }else{
                this.isUser = true;
                let params: URLSearchParams = new URLSearchParams();
                params.set('ownerId', this.authService.userProfile.user_id);
                this.http.get('api/notification/getnotificationsorder', {
                    search: params
                }).subscribe(
                (response) => {this.notifications=response.json();this.done=true}, 
                (error) => console.log("error")
            );
            }
       }else{
            this.router.navigate(['home']);
        }   
    }

    public onOk(item){
         let params: URLSearchParams = new URLSearchParams();
            params.set('id', this.notifications[item].notificationId);
            params.set('status',"OK");
            this.http.get('api/notification/SetNotificationStatus', {
                    search: params
                }).subscribe(
                (response) => {this.notifications=response.json()}, 
                (error) => console.log("error")
            );
    }

    public onReject(item){
         let params: URLSearchParams = new URLSearchParams();
            params.set('id', this.notifications[item].notificationId);
            params.set('status', "Rejected");
            this.http.get('api/notification/SetNotificationStatus', {
                    search: params
                }).subscribe(
                (response) => {
                    let params: URLSearchParams = new URLSearchParams();
                    params.set('ownerId', this.authService.userProfile.user_id);
                    this.http.get('api/notification/getnotificationsorder', {
                        search: params
                    }).subscribe(
                    (response) => {this.notifications=response.json();this.done=true}, 
                    (error) => console.log("error")
                );
            }, 
                (error) => console.log("error")
            );
    }

    public onSelect(item) {
        console.log(item);
        console.log(this.notifications[item].condition);
        if(!this.notifications[item].condition){
            let params: URLSearchParams = new URLSearchParams();
            params.set('id', this.notifications[item].orderId);
            this.http.get('api/order/orderById', {
                    search: params
                }).subscribe(
                (response) => {this.notifications[item].taxiOrder=response.json();this.notifications[item].isLoad=true;this.notifications[item].condition=!this.notifications[item].condition;}, 
                (error) => console.log("error")
            );
        }else{
            this.notifications[item].condition=!this.notifications[item].condition;
        }
    }

    public onAgree(item){
        this.notifications[item].conditionAgree=!this.notifications[item].conditionAgree;
    }

    public onAgreed(item,form){
        let params1: URLSearchParams = new URLSearchParams();
            params1.set('receiverId',this.notifications[item].receiverId);
            console.log(this.notifications[item].receiverId);
        let params2: URLSearchParams = new URLSearchParams();
            params2.set('id',this.notifications[item].notificationId);    
        let params: URLSearchParams = new URLSearchParams();
            params.set('id', this.notifications[item].orderId);
            params.set('duration',form.value.duration);
            params.set('distanse',form.value.distanse);
            
            this.http.get('api/offer/GetTaxiOfferByOwner', {
                    search: params1
                }).subscribe(
                (response) => {
                    params.set('price',response.json().price);
                    this.http.get('api/order/confirmTaxiOrder', {
                            search: params
                        }).subscribe(
                            (response) => {this.price=response.json();
                            params2.set('status',"Agreed");
                            this.http.get('api/notification/SetNotificationStatus', {
                                    search: params2
                                }).subscribe(
                                (response) => {
                                    this.http.get('api/notification/getnotificationsorder', {
                                            search: params
                                        }).subscribe(
                                        (response) => {this.notifications=response.json();this.done=true}, 
                                        (error) => console.log("error")
                                    ); 
                                }, 
                                (error) => console.log("error")
                            );
                    }, 
                (error) => console.log("error")
            );
        },
        (error) => console.log("error")); 
    }
}


