import { Router } from 'aurelia-router';
import { LogManager, autoinject, bindable } from 'aurelia-framework';
import 'bootstrap';
import { IParcel } from 'domain/IParcel';

export var log = LogManager.getLogger('app.components.parcel-list');

@autoinject
export class ParcelListCustomElement {
  @bindable finalized: boolean;
  @bindable parcels: IParcel[];

  constructor(
    private router: Router
  ) { }

  editParcel(parcel: IParcel) {
    this.router.navigateToRoute('parcelEdit', { number: parcel.number });
  }

}
