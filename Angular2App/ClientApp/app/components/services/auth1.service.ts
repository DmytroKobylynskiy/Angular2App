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
      primaryColor: 'blue'
    },
    languageDictionary: {
      emailInputPlaceholder: "Введите ваш email",
      title: "Вход"
    } 
  };
@Injectable()
export class Auth1Service {
  userProfile: any;
  lock = new Auth0Lock('XpkhVEtgPJir5eq9LzQaVct8EIbMpCz6', 'djigli.eu.auth0.com',options);

  constructor(private http: Http,private router: Router) {
    this.userProfile = JSON.parse(localStorage.getItem('profile'));

    // Add callback for lock `authenticated` event
    this.lock.on("authenticated", (authResult) => {
      localStorage.setItem('id_token', authResult.idToken);

      // Fetch profile information
      this.lock.getProfile(authResult.idToken, (error, profile) => {
        if (error) {
          // Handle error
          alert(error);
          return;
        }

        profile.user_metadata = profile.user_metadata || {role:"customer"};
        localStorage.setItem('profile', JSON.stringify(profile));
        this.userProfile = profile;
      });
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

  getUserProfile(){
    return this.userProfile;
  }
}