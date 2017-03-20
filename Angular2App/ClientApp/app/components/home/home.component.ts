import { Component, ChangeDetectorRef } from '@angular/core';
import { Validators, FormGroup, FormArray, FormBuilder } from '@angular/forms';
import { Ng2MapComponent } from 'ng2-map';

@Component({
    selector: 'home',
    template: require('./home.component.html')
})
export class HomeComponent {
    autocomplete: any;
    address: any = {};
    center: any;

  constructor(private ref: ChangeDetectorRef) {}
    initialized(autocomplete: any) {
        console.log("2132123123");
        this.autocomplete = autocomplete;
    }
    placeChanged(place) {
        this.center = place.geometry.location;
        for (let i = 0; i < place.address_components.length; i++) {
        let addressType = place.address_components[i].types[0];
        this.address[addressType] = place.address_components[i].long_name;
        }
        this.ref.detectChanges();
    }
}