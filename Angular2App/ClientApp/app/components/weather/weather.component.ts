import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'weather',
    template: require('./weather.component.html')
})
export class WeatherComponent {
    public weather: Weather;
    public clients : string;

    constructor(private http: Http) {
    }

    public getWeather(chosenCity: string) {
        this.http.get('/api/weather/city/' + chosenCity).subscribe(result => {
            this.weather = result.json();
        });
    }

    public getUser(){
        this.http.get('/api/users/clients').subscribe(result=>{
            this.clients = result.json();
        })
    }
}

interface Weather {
    temp: string;
    summary: string;
    city: string;
}

interface Client {
    carExist: boolean;
    driverLicense: string;
}