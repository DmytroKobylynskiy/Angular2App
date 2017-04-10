import { Component, ChangeDetectorRef } from '@angular/core';
import { Validators, FormGroup, FormArray, FormBuilder } from '@angular/forms';
import { Ng2MapComponent } from 'ng2-map';

@Component({
    selector: 'home',
    template: require('./home.component.html')
})
export class HomeComponent {
  constructor(private ref: ChangeDetectorRef) {}
    
}