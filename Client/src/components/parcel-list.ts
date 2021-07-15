import { ParcelService } from 'services/parcel-service';
import { Router } from 'aurelia-router';
import { LogManager, autoinject, bindable } from 'aurelia-framework';
import 'bootstrap';
import { IParcel } from 'domain/IParcel';
import { Utils } from 'helpers/Utils';
import { EventAggregator } from 'aurelia-event-aggregator';
import { PostOfficeEvent } from 'helpers/PostOfficeEvent';

export var log = LogManager.getLogger('app.components.parcel-list');

@autoinject
export class ParcelListCustomElement {
  @bindable finalized: boolean;
  @bindable parcels: IParcel[];

  @bindable errorTitle: string;
  @bindable errorDetails: string;

  constructor(
    private router: Router,
    private parcelService: ParcelService,
    private eventAggregator: EventAggregator
  ) { }

  editParcel(parcel: IParcel) {
    this.router.navigateToRoute('parcelEdit', { number: parcel.number });
  }

  deleteParcel(parcel: IParcel) {
    this.errorTitle = undefined;
    this.errorDetails = undefined;

    this.parcelService.delete(parcel.number)
      .then(_ => this.eventAggregator.publish(PostOfficeEvent.ReloadHome))
      .catch(error => {
        console.log(error);

        Utils.getErrors(error).then(errors => {
          console.log(errors);

          this.errorTitle = Object.keys(errors)[0];
          this.errorDetails = Object.values(errors)[0][0] as string;
        });
      });
  }
}
