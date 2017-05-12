import { DialogRef, ModalComponent } from 'angular2-modal';
import { BSModalContext } from 'angular2-modal/plugins/bootstrap';
import { Component } from "@angular/core";

export class AdditionCalculateWindowData extends BSModalContext {
  constructor(public date: string, public time: string) {
    super();
  }
}

/**
 * A Sample of how simple it is to create a new window, with its own injects.
 */
@Component({
  selector: 'angular2app',
  styles: [require('./taxiorders.component.css')],
  //TODO: [ngClass] here on purpose, no real use, just to show how to workaround ng2 issue #4330.
  // Remove when solved.
  /* tslint:disable */ template: `
        <div >
            <div >
                <div>
                    <h1>A Custom modal design</h1>
                </div>
            </div>
            <div >
                <div >
                    <div>
                        <h1>Do the math to quit:</h1>
                        <span>What is the sum?</span>
                         <input class="form-control" type="date" #date  autofocus>
                         <input class="form-control" type="time" #time  autofocus>
                         <button  class="btn-primary">OK</button>
                    </div>
                </div>
            </div>
        </div>`
})
export class AdditionCalculateWindow implements ModalComponent<AdditionCalculateWindowData> {
  context: AdditionCalculateWindowData;

  constructor(public dialog: DialogRef<AdditionCalculateWindowData>) {
      dialog.setCloseGuard(27);
  }

}