import { Injectable, ChangeDetectorRef } from '@angular/core';
import {Http} from '@angular/http';
import {Response, Headers} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
 import { NgForm} from '@angular/forms';
@Injectable()
export class MapsService{
 
    constructor(private http: Http,private ref: ChangeDetectorRef){ 
        //console.log("Constr"+this.positions);
    }
    //public longitude ?: any;
    //public latitude ?: any;
    public positions = [  ];
    onMapReady(map) {
        console.log('map', map);
        console.log('markers', map.markers);  
                console.log("On map ready"+this.positions);// to get all markers as an array 
    }
    onIdle(event) {
        console.log('map', event.target);
    }
    onMarkerInit(marker) {
        console.log('marker', marker);
    }
    onMapClick(event) {
        this.positions.push(event.latLng);
        console.log("On map click"+event.latLng);
        event.target.panTo(event.latLng);
    }

    clicked(event) {
        let marker = event.target;
        marker.ng2MapComponent.openInfoWindow('iw', marker, {
        lat: marker.getPosition().lat(), lng: marker.getPosition().lng(),
        });
    }

    public getPos(){
        return this.positions;
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
        /*console.log([position.coords.latitude, position.coords.longitude]);
                console.log("SC"+this.positions);*/
        this.positions.push([position.coords.latitude, position.coords.longitude]);
    }
    

    autocomplete: any;
    address: any = {};
    center: any;

    initialized(autocomplete: any) {
        this.autocomplete = autocomplete;
    }
    placeChanged=(place)=> {
        
        this.center = place.geometry.location;
        for (let i = 0; i < place.address_components.length; i++) {
            let addressType = place.address_components[i].types[0];
            this.address[addressType] = place.address_components[i].long_name;
        }
        
        if(this.checkExist([this.center.lat(), this.center.lng()].toString())){
            this.positions.push([this.center.lat(), this.center.lng()]);
        }
        //       
        this.ref.detectChanges();
    }

    public checkExist=(str)=>{
        for (let i = 0; i < this.positions.length; i++) {
            if(this.positions[i].toString()==str)
                return false;
        }
        return true;
    }
    
}