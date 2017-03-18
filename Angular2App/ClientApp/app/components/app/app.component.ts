import { Component } from '@angular/core';

@Component({
    selector: 'app',
    template: require('./app.component.html'),
    styles: [require('./app.component.css')]
})
export class AppComponent {
    lat: number = 51.678418;
    lng: number = 7.809007;
    
}
