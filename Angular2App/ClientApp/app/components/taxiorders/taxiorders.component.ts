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
@Component({
    selector: 'angular2app',
    template: require('./taxiorders.component.html'),
    providers: [HttpService,Auth1Service],
    styles: [require('./taxiorders.component.css')]
})

export class TaxiOrdersComponent {
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
    constructor(private auth: Auth1Service,private http: Http,private httpService: HttpService,private route: ActivatedRoute,
        private router: Router,private authService: Auth1Service,overlay: Overlay, vcRef: ViewContainerRef, public modal: Modal) {
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
            this.http.get('/api/order/taxiorders').subscribe(result => {
                this.taxiOrders = result.json();
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
            console.log("sdseq312");
            let params: URLSearchParams = new URLSearchParams();
            params.set('id', item.id);
            params.set('receiverId',this.authService.userProfile.user_id);
            console.log(item.id);
            this.http.get('api/order/agreetaxiorder', {
                search: params
            }).subscribe(
                (response) => {this.taxiOrder = response.json();this.done=true;console.log(this.taxiOrder);}, 
                (error) => console.log("error")
            );
        }
    }
}


