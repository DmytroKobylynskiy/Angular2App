import { Injectable } from '@angular/core';
import { tokenNotExpired } from 'angular2-jwt';
import { Router } from '@angular/router';
import { Http } from '@angular/http';
import {Response, Headers} from '@angular/http';
import {Observable} from 'rxjs/Observable';
let Auth0Lock = require('auth0-lock').default;
var options = {
    language: 'ru',
    theme: {
      logo: 'https://eschool.alruyaschool.net/rbs_eschool/Website/Images/Login_Key.png',
      primaryColor: 'green'
    },
    languageDictionary: {
      emailInputPlaceholder: "something@youremail.com",
      title: "Ша войду"
    },
    additionalSignUpFields: [{
      name: "driveLicense",                              // required
      placeholder: "enter your driveLicense",            // required
      driveLicense : "enter your driver license",
      validator: function(value) {                  // optional
        // only accept addresses with more than 10 characters
        return value.length > -1;
      }
    },
    {
        type: "select",
        name: "role",
        options: [
          {value: "customer", label: "Заказчик"},
          {value: "сarrier", label: "Перевозчик"}
        ],
        prefill: "Заказчик",
        placeholder: "Заказчик или предлагающий?"
    }] 
  };
@Injectable()
export class AuthService {
  
  lock = new Auth0Lock('XpkhVEtgPJir5eq9LzQaVct8EIbMpCz6', 'djigli.eu.auth0.com',options);

  constructor(private http: Http,private router: Router) {
    this.lock.on('authenticated', (authResult: any) => {
      localStorage.setItem('id_token', authResult.idToken);
      console.log(authResult);
      this.lock.getProfile(authResult.idToken, (error: any, profile: any) => {
        if (error) {
          console.log(error);
        }
        console.log(profile.user_metadata.role);

        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        console.log(authResult.idToken);
        const body = JSON.stringify(profile);
        var response = null;
         this.http.post('/api/account/getProfile',body,headers).subscribe(result=>{
            response = result.json();
        })           
        localStorage.setItem('profile', JSON.stringify(profile));
      });

      this.lock.hide();
    });
  }

  login() {
    this.lock.show();
  }

  logout() {
    // To log out, just remove the token and profile
    // from local storage
    localStorage.removeItem('profile');
    localStorage.removeItem('id_token');

    // Send the user back to the dashboard after logout
    this.router.navigateByUrl('/home');
  }

  loggedIn() {
    return tokenNotExpired();
  }
}