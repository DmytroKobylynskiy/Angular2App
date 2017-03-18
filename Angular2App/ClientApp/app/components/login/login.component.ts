import { Component } from "@angular/core";
import { Router } from '@angular/router';

import { AuthService } from "../services/auth.service";

@Component({
    moduleId: module.id,
    selector: "my-login",
    templateUrl: "view.html",
    styleUrls: ["style.css"]
})
export class LoginComponent {

    private userName: string;
    private password: string;

    constructor(
        private authService: AuthService,
        private router: Router
    ) { }
}