import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CanActivate } from '@angular/router';
import { Auth1Service } from './auth1.service';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private auth: Auth1Service, private router: Router) {}

  canActivate() {
    if (!this.auth.loggedIn()) {
      this.router.navigate(['']);
      return false;
    }
    return true;
  }

}