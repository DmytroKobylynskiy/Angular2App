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
    public end_positions = [  ];
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

    public getEndPos(){
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
        /*console.log([position.coords.latitude, position.coords.longitude]);*/
                console.log("SC"+position);
        this.positions[0]=[position.coords.latitude, position.coords.longitude];
    }
    

    autocomplete: any;
    address: any = {};
    public adresa = [];
    center: any;
    end_center: any;
    initialized(autocomplete: any) {
        this.autocomplete = autocomplete;
    }
    placeChanged=(place)=> {
        
        this.center = place.geometry.location;
        for (let i = 0; i < place.address_components.length; i++) {
            let addressType = place.address_components[i].types[0];
            this.address[addressType] = place.address_components[i].long_name;
            this.adresa.push( place.address_components[i].long_name);
        }
        console.log(this.adresa.toString());
        if(this.checkExist([this.center.lat(), this.center.lng()].toString())){
            this.positions[0]=[this.center.lat(), this.center.lng()];
        }
        this.adresa = [];
        //       
        this.ref.detectChanges();
    }

    endPlaceChanged=(place)=> {
        
        this.center = place.geometry.location;
        for (let i = 0; i < place.address_components.length; i++) {
            let addressType = place.address_components[i].types[0];
            this.address[addressType] = place.address_components[i].long_name;
        }
        
        if(this.checkExist([this.center.lat(), this.center.lng()].toString())){
            this.end_positions[0]=[this.center.lat(), this.center.lng()];
        }
        //       
        this.ref.detectChanges();
    }

    public getAdresa=()=>{
        console.log(this.adresa);
        return this.adresa.toString();
    }

    public checkExist=(str)=>{
        for (let i = 0; i < this.positions.length; i++) {
            if(this.positions[i].toString()==str)
                return false;
        }
        return true;
    }

    public endCheckExist=(str)=>{
        for (let i = 0; i < this.end_positions.length; i++) {
            if(this.end_positions[i].toString()==str)
                return false;
        }
        return true;
    }
    
}