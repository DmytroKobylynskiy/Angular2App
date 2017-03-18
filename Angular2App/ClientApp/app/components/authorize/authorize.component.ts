import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { NgForm} from '@angular/forms';
import {Response, Headers, URLSearchParams} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import { HttpService} from './http.service';
import {Router} from '@angular/router';
@Component({
    selector: 'angular2app',
    template: require('./authorize.component.html'),
    providers: [HttpService]
})

export class AuthorizeComponent {
    public str : string;
    public done : boolean ;
    public condition: boolean=false;
    constructor(private http: Http,private httpService: HttpService,private router: Router) {
    }
    login(form : NgForm){
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        this.condition = true;
        this.httpService.postData(form)
                .subscribe((data) => {this.str=data; this.done=true;});
        console.log("login ok");
        this.router.navigate(['./weather']);
    }
    
    
}
