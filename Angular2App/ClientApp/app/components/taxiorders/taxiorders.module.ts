import { NgModule }       from '@angular/core';
import { CommonModule }   from '@angular/common';
import { FormsModule }    from '@angular/forms';

import { TaxiOrdersComponent } from './taxiorders.component';
import { EditOrderComponent }  from './editorder.component';
import { TaxiRoutingModule } from './taxi-routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    TaxiRoutingModule
  ],
  declarations: [
    TaxiOrdersComponent,
    EditOrderComponent
  ]
})
export class TaxiOrdersModule {}
