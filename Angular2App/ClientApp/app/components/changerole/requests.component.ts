import { Component } from '@angular/core';
import { Auth1Service } from '../services/auth1.service';
import { Http } from '@angular/http';
import { AuthHttp } from 'angular2-jwt';
import { myConfig } from '../auth.config';
import { Router } from '@angular/router';
import { NgForm} from '@angular/forms';
import '../entities/changerolerequest'
import { HttpService } from "../services/http.service";
import {Response, Headers, URLSearchParams} from '@angular/http';
@Component({
    selector: 'requests',
    template: require('./requests.component.html'),
    providers: [Auth1Service,HttpService],
    styles: [require('./requests.component.css')]
})
export class RequestsComponent {
    roleUser : any;
    changeRoleRequest : ChangeRoleRequest;
    changeRoleRequests : Array<ChangeRoleRequest>;
    done : boolean;
    constructor(private http: Http,private auth: Auth1Service, private authHttp: AuthHttp,private router: Router) {
        console.log(this.auth.userProfile);
    }

    ngOnInit(){
        if(this.auth.loggedIn()){
            this.http.get('/api/role/changeRoleRequests').subscribe(result => {
                this.changeRoleRequests = result.json();
            });
        }else{
            this.router.navigate(['home']);
        }   
    }

    onReject(item) {
       let params: URLSearchParams = new URLSearchParams();
        params.set('id', item+1);
        params.set('status',"Rejected");
        this.http.get('api/role/SetRequestStatus', {
                search: params
            }).subscribe(
            (response) => {this.done=true;
                var index = this.changeRoleRequests.indexOf(item, 0);
                if (index > -1) {
                    this.changeRoleRequests.splice(index, 1);
                }
            }, 
            (error) => console.log("error")
        );
    }

    onAgree(item){
        let params: URLSearchParams = new URLSearchParams();
        var request : ChangeRoleRequest;
        params.set('id', item+1);
        params.set('status', "Аgreed");
        this.http.get('api/role/SetRequestStatus', {
                search: params
            }).subscribe(
            (response) => {request=response.json();
                            var data = JSON.stringify({
                                user_metadata: {
                                    role: request.newRole,
                                    driveLicense: request.driverLicense
                                }
                            });
                            this.authHttp
                            .patch('https://' + myConfig.domain + '/api/v2/users/' + request.requestOwnerId, data)
                            .map(response => response.json())
                            .subscribe(
                                response => {
                                    console.log(response);
                                },
                                error => alert(error.json().message)
                            );
                            this.done=true}, 
            (error) => console.log("error")
        );
    }
}
