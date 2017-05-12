import { Component } from '@angular/core';
import { Auth1Service } from '../services/auth1.service';
import { Http } from '@angular/http';
import { AuthHttp } from 'angular2-jwt';
import { myConfig } from '../auth.config';
import { Router } from '@angular/router';
import { NgForm} from '@angular/forms';
import '../entities/changerolerequest'
import { HttpService } from "../services/http.service";
@Component({
    selector: 'change-role',
    template: require('./change-role.component.html'),
    providers: [Auth1Service,HttpService]
})
export class ChangeRoleComponent {
    roleUser : any;
    changeRoleRequest : ChangeRoleRequest;
    done : boolean;
    constructor(private http: HttpService,private auth: Auth1Service, private authHttp: AuthHttp,private router: Router) {
        console.log(this.auth.userProfile);
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
                    },
                    error => alert(error.json().message)
                );
            }else{
                console.log("if2");
                form.value.newRole = this.roleUser;
                form.value.requestOwnerId = this.auth.userProfile.user_id;
                this.http.postChangeRoleRequest(form)
                    .subscribe((data) => {console.log(data);this.done=true;});
            }
        }
        
    }
}
