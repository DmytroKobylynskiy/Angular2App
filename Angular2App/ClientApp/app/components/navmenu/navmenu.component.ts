import { Component } from '@angular/core';
import { Auth1Service } from './auth1.service';
import { Http } from '@angular/http';
import { HttpService} from './http.service';
@Component({
    selector: 'nav-menu',
    template: require('./navmenu.component.html'),
    styles: [require('./navmenu.component.css')],
    providers: [HttpService,Auth1Service]
})
export class NavMenuComponent {

    private userRole : string;
    public loggedIn : boolean;
    constructor(private http: Http,private httpService: HttpService,private authService: Auth1Service) {
    }

    public login() {
        
    }

    public getUserRole() {
        this.http.get('/api/account/profile').subscribe(result => {
            this.userRole = result.json ? result.json() : result;
        });
    }
}
