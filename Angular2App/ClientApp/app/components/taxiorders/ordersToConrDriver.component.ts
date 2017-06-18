import { Http } from '@angular/http';
import { HttpService} from '../services/http.service';
import { NgForm} from '@angular/forms';
import {Response, Headers, URLSearchParams} from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/catch';
import './taxiorders';
import { Auth1Service } from "../services/auth1.service";
import { Component, ViewContainerRef } from '@angular/core';
import { Overlay } from 'angular2-modal';
import { Modal } from 'angular2-modal/plugins/bootstrap';
import { AdditionCalculateWindow, AdditionCalculateWindowData } from "./modal";
import { NotificationService } from "../services/notification.service";
import { AuthHttp } from "angular2-jwt/angular2-jwt";
import { myConfig } from "../auth.config";
import { OffersService } from "../services/offers.service";
@Component({
    selector: 'angular2app',
    template: require('./ordersToConrDriver.component.html'),
    providers: [HttpService,Auth1Service,NotificationService,OffersService],
    styles: [require('./taxiorders.component.css')]
})

export class OrdersToConrDriver {
    public taxiOrders: Observable<Array<TaxiOrder>>;
    public taxiOrdersId: Observable<Array<TaxiOrder>>;
    public taxiOrder : Observable<TaxiOrder>;
    public authorized : boolean;
    public done : boolean ;
    private selectedId: number;
    public condition: boolean=false;
    private isAdmin : boolean = false;
    private isCarrier : boolean = false;
    private isUser : boolean = false;
    private valid : boolean;
    public order : string;
    reverse: boolean = false;
    constructor(private offersService:OffersService,private notificationService:NotificationService,private auth: Auth1Service,private http: Http,private httpService: HttpService,private route: ActivatedRoute,
        private router: Router,private authService: Auth1Service,overlay: Overlay, vcRef: ViewContainerRef, public modal: Modal,private authHttp: AuthHttp) {
            overlay.defaultViewContainer = vcRef;
            this.authorized=this.auth.loggedIn();
    }
    createTaxiOrder(form : NgForm){
        const body = JSON.stringify(this.taxiOrder);
        console.log(form.value.startPoint);
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        this.condition = true;
        this.httpService.postData(form)
                .subscribe((data) => { this.done=true;});
    }
    ngOnInit() {
        if(this.auth.loggedIn()){
            let params: URLSearchParams = new URLSearchParams();
            params.set('userId', this.auth.getUserProfile().user_id);
            this.http.get('/api/order/myrequests', {
                search: params
            }).subscribe(result => {
                this.taxiOrders = result.json();
                console.log(this.taxiOrders);
            });
            
        
            this.taxiOrdersId = this.route.params.switchMap((params: Params) => {
                this.selectedId = +params['id'];
                console.log("ddsd"+this.selectedId);
                return this.taxiOrders;
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
        this.taxiOrders[item].condition = !this.taxiOrders[item].condition;
        
    }

    public onEdit(item) {
        console.log(item.id);
        this.router.navigate(['taxiorders', item.id]);
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

    public onAgree(item) {
        console.log(item);
        this.taxiOrders[item].conditionAgree=!this.taxiOrders[item].conditionAgree;
    }

    setOrder(value: string) {
        if (this.order === value) {
            this.reverse = !this.reverse;
        }
        this.order = value;
    }

    public onAgreed(form: NgForm,item) {
        var displayDate = new Date().getFullYear().toString();
        var displayTime = new Date().getHours().toString()+":"+new Date().getMinutes().toString();
        if(new Date().getHours()<=10){
            displayTime = 0+new Date().getHours().toString()+":"+new Date().getMinutes().toString();
        }else{
            displayTime = new Date().getHours().toString()+":"+new Date().getMinutes().toString();
        }
        if(new Date().getMinutes()<=10){
            displayTime = new Date().getHours().toString()+":"+0+new Date().getMinutes().toString();
        }else{
            displayTime = new Date().getHours().toString()+":"+new Date().getMinutes().toString();
        }
        console.log(displayTime);
        console.log(form.value.time);
        var month = new Date().getUTCMonth()+1;
        
        if(month<=10){
            displayDate = displayDate+"-"+0+month.toString();
        }else{
            displayDate = displayDate+"-"+month.toString();
        }
        if(new Date().getDate()<=10){
            displayDate = displayDate+"-"+0+new Date().getDate().toString();
        }else{
            displayDate = displayDate+"-"+new Date().getDate().toString();
        }
        console.log(displayDate);
        //console.log(date);
        this.valid = displayDate>form.value.date;
        console.log(this.valid);
        if(this.valid==false){
            let params: URLSearchParams = new URLSearchParams();
            params.set('id', item.id);
            params.set('receiverId',this.authService.userProfile.user_id);
            params.set('receiverEmail',this.authService.userProfile.email);
            console.log(item.id);
            this.http.get('api/order/agreetaxiorder', {
                search: params
            }).subscribe(
                (response) => {this.taxiOrder = response.json();this.done=true;this.notificationService.getNumNotifications(this.auth.userProfile.user_id);}, 
                (error) => console.log("error")
            );
            this.offersService.busyOffer(this.authService.userProfile.user_id,form.value.date,form.value.time);
            var data = JSON.stringify({
                                user_metadata: {
                                    busyDate: form.value.date,
                                    busyTime : form.value.time
                                }
                            });
            this.authHttp
            .patch('https://' + myConfig.domain + '/api/v2/users/' + this.authService.userProfile.user_id, data)
            .map(response => response.json())
            .subscribe(
                    response => {
                        this.auth.userProfile = response;
                        localStorage.setItem('profile', JSON.stringify(response));
                        this.router.navigate(['/profile']);
                        
                    },
                    error => alert(error.json().message)
            );
        }
    }
}


