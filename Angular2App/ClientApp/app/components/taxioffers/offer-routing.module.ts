import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TaxiOffersComponent }    from './taxioffers.component';
import { EditOfferComponent }  from './editoffer.component';

const taxiOfferRoutes: Routes = [
  { path: 'taxioffer',  component: TaxiOffersComponent },
  { path: 'taxioffer/:id', component: EditOfferComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(taxiOfferRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class OfferRoutingModule {
    constructor(){
        console.log("TaxiRoutingModule");
    }
}
