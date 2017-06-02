import { Component, NgZone } from '@angular/core';
import { Auth1Service } from '../services/auth1.service';
import { Http } from '@angular/http';
import { AuthHttp } from 'angular2-jwt';
import { myConfig } from '../auth.config';
import { Router } from '@angular/router';
import { NgForm} from '@angular/forms';

import '../entities/changerolerequest'
import { HttpService } from "../services/http.service";
import {Response, Headers, URLSearchParams} from '@angular/http';
import { OffersService } from "../services/offers.service";
import { OrdersService } from "../services/orders.service";
@Component({
    selector: 'profile',
    template: require('./profile.component.html'),
    providers: [Auth1Service,HttpService,OffersService,OrdersService],
    styles: [require('./profile.component.css')]
})
export class ProfileComponent {
    roleUser: any;
    changeRoleFlag : boolean = false;
    picture : any;
    isUser : boolean;
    isCarrier : boolean;
    email : string;
    name : string;
    done : boolean;
    myOrders : any;
    myOrder : TaxiOrder
    constructor(private zone :NgZone, private http: Http,private httpService: HttpService,private auth: Auth1Service, private authHttp: AuthHttp,private router: Router,
         private orderService : OrdersService,private offerService : OffersService) {

    }
    
    ngOnInit(){
        this.email = this.auth.userProfile.email;
        this.name = this.auth.userProfile.name;
        this.picture = this.auth.userProfile.picture;
        this.roleUser = this.auth.userProfile.user_metadata.role;
        console.log(this.roleUser);
        let params: URLSearchParams = new URLSearchParams();
        
        if(this.roleUser=="customer"){
            params.set('id', this.auth.userProfile.user_id);  
            this.isUser = true;
            this.http.get('/api/order/myorders',{
                search: params}
            ).subscribe(result => {
                this.myOrders = result.json();
                this.myOrder = this.myOrders[0];
                this.done = true;
                console.log(this.myOrders);
            });
        }else{
            params.set('ownerId', this.auth.userProfile.user_id);  
            this.isCarrier = true;
            this.http.get('/api/offer/myoffers',{
                search: params}
            ).subscribe(result => {
                this.myOrders = result.json();
                this.myOrder = this.myOrders[0];
                this.done = true;
                console.log(this.myOrders);
            });
        }     
        
    }

    public onSelect(item) {
        console.log(item);
        this.myOrders[item].condition = !this.myOrders[item].condition;
        
    }



    public onEdit(item) {
        if(this.isCarrier){
            this.router.navigate(['offers', item.id]);
        }
        if(this.isUser){
             this.router.navigate(['taxiorders', item.id]);
        }
    }

    changeRole(){
        this.changeRoleFlag = !this.changeRoleFlag;
    }

    onSubmit(form : NgForm) {
        var headers: any = {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        };
        var data : any;
        console.log(this.roleUser);
        if(this.roleUser==this.auth.getUserProfile().user_metadata.role){

        }else{
            if(this.roleUser=="customer"){
                console.log("if1");
                data = JSON.stringify({
                    user_metadata: {
                        role: this.roleUser,
                        driveLicense: form.value.driverLicense
                    }
                });
                this.authHttp
                .patch('https://' + myConfig.domain + '/api/v2/users/' + this.auth.userProfile.user_id, data)
                .map(response => response.json())
                .subscribe(
                    response => {
                    this.auth.userProfile = response;
                    localStorage.setItem('profile', JSON.stringify(response));
                    this.router.navigate(['/profile']);
                    this.zone.runOutsideAngular(() => {
                        location.reload();
                    });
                    },
                    error => alert(error.json().message)
                );
            }else{
                console.log("if2");
                form.value.newRole = this.roleUser;
                form.value.requestOwnerId = this.auth.userProfile.user_id;
                this.httpService.postChangeRoleRequest(form)
                    .subscribe((data) => {console.log(data);this.done=true;this.zone.runOutsideAngular(() => {
                        location.reload();
                    });});
            }
        }
        
    }
}
