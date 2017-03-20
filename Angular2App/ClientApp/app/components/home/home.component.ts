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
    positions = [];
  constructor(private ref: ChangeDetectorRef) {}
    initialized(autocomplete: any) {
        console.log("2132123123");
        this.autocomplete = autocomplete;
    }
    placeChanged=(place)=> {
        
        this.center = place.geometry.location;
        for (let i = 0; i < place.address_components.length; i++) {
        let addressType = place.address_components[i].types[0];
        this.address[addressType] = place.address_components[i].long_name;
        }
        this.positions.push([this.center.lat(),this.center.lng()]);
        this.ref.detectChanges();
    }
    showRandomMarkers() {
    let randomLat: number, randomLng: number;
    this.positions = [];
    for (let i = 0 ; i < 9; i++) {
      randomLat = Math.random() * 0.0099 + 43.7250;
      randomLng = Math.random() * 0.0099 + -79.7699;
      this.positions.push([randomLat, randomLng]);
    }
  }
  nav = null;
  requestPosition=()=> {
        if (this.nav == null) {
            this.nav = window.navigator;
        }
        if (this.nav != null) {
            var geoloc = this.nav.geolocation;
            if (geoloc != null) {
                geoloc.getCurrentPosition(this.successCallback);
            }
            else {
                alert("geolocation not supported");
            }
        }
        else {
            alert("Navigator not found");
        }
    }
    successCallback=(position)=> {
        //console.log(position.coords.longitude);
        console.log([position.coords.latitude, position.coords.longitude]);
                console.log("SC"+this.positions);
        this.positions.push([position.coords.latitude, position.coords.longitude]);
    }
}