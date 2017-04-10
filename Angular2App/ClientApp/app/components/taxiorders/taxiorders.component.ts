import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { HttpService} from './http.service';
import { NgForm} from '@angular/forms';
import {Response, Headers, URLSearchParams} from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import './taxiorders';

@Component({
    selector: 'angular2app',
    template: require('./taxiorders.component.html'),
    providers: [HttpService]
})

export class TaxiOrdersComponent {
    public taxiOrders: Array<TaxiOrder>;
    public taxiOrder : TaxiOrder;
    public str : string;
    public done : boolean ;
    public condition: boolean=false;
    public condition2: boolean=false;
    constructor(private http: Http,private httpService: HttpService,private route: ActivatedRoute,private router: Router) {
    }
    createTaxiOrder(form : NgForm){
        const body = JSON.stringify(this.taxiOrder);
        console.log(form.value.startPoint);
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        this.condition = true;
        this.httpService.postData(form)
                .subscribe((data) => {this.str=data; this.done=true;});
        
    }
    public getTaxiOrder() {
        this.http.get('/api/order/taxiorders').subscribe(result => {
            this.taxiOrders = result.json();
        });
    }

    public detailsOrder(item)
    {
        console.log(item.id);
        this.taxiOrders[item.id-1].condition = !this.taxiOrders[item.id-1].condition;
       /* this.http.get('/api/order/detailstaxiorder').subscribe(result => {
            this.taxiOrders = result.json();
        });*/
    }

    submit(form: NgForm){
        console.log(form.value.startPoint);
    }

    private selectedId: number;


    ngOnInit() {
        this.http.get('/api/order/taxiorders').subscribe(result => {
                this.taxiOrders = result.json();
            });
    }

    public onSelect(item) {
        console.log(item.id);
        this.router.navigate(['taxiorders', item.id]);
    }
    
}


