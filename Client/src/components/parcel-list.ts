import { LogManager, autoinject, bindable } from 'aurelia-framework';
import 'bootstrap';
import { IParcel } from 'domain/IParcel';

export var log = LogManager.getLogger('app.components.parcel-list');

@autoinject
export class ParcelListCustomElement {
  @bindable parcels: IParcel[];
}
