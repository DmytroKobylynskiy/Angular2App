import { Component } from '@angular/core';
import { Auth1Service } from './auth1.service';
import { Http } from '@angular/http';
import { AuthHttp } from 'angular2-jwt';
import { myConfig } from '../auth.config';
import { Router } from '@angular/router';
import { NgForm} from '@angular/forms';
@Component({
    selector: 'change-role',
    template: require('./change-role.component.html'),
    providers: [Auth1Service]
})
export class ChangeRoleComponent {
    roleUser : any;
    constructor(private auth: Auth1Service, private authHttp: AuthHttp,private router: Router) {
        console.log(this.auth.userProfile);
    }

    onSubmit(form : NgForm) {
        var headers: any = {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        };
        
        console.log(this.roleUser);
        var data: any = JSON.stringify({
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
    }
}
