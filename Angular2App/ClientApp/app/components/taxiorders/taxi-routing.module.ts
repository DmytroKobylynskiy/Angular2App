import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TaxiOrdersComponent }    from './taxiorders.component';
import { EditOrderComponent }  from './editorder.component';

const taxiOrderRoutes: Routes = [
  { path: 'taxiorder',  component: TaxiOrdersComponent },
  { path: 'taxiorder/:id', component: EditOrderComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(taxiOrderRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class TaxiRoutingModule {
    constructor(){
        console.log("TaxiRoutingModule");
    }
 }
