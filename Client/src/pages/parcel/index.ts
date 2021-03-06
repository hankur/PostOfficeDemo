import { IParcel } from './../../domain/IParcel';
import { IBag } from 'domain/IBag';
import { Router } from 'aurelia-router';
import { LogManager, autoinject } from "aurelia-framework";
import { ParcelService } from 'services/parcel-service';
import { BagService } from 'services/bag-service';
import { json } from 'aurelia-fetch-client';
import { Utils } from 'helpers/Utils';

export var log = LogManager.getLogger('app.parcel.index');

@autoinject
export class Index {
  bags: IBag[] | undefined;

  number: string;
  bagNumber: string;
  recipient: string;
  destination: string;
  weight: string;
  price: string;

  errorTitle: string;
  errorDetails: string;

  constructor(
    private parcelService: ParcelService,
    private bagService: BagService,
    private router: Router
  ) {
    this.loadBags();
  }

  loadBags() {
    this.errorTitle = undefined;
    this.errorDetails = undefined;

    this.bagService.fetchAll<IBag>().then(result => {
      this.bags = result;

      console.log(this.bags);
    }).catch(error => {
      Utils.getErrors(error).then(errors => {
        console.log(errors);

        this.errorTitle = Object.keys(errors)[0];
        this.errorDetails = Object.values(errors)[0][0] as string;
      });
    });
  }

  submit() {
    this.errorTitle = undefined;
    this.errorDetails = undefined;

    let parcel: IParcel = {
      number: this.number,
      bagNumber: this.bagNumber,
      recipient: this.recipient,
      destination: this.destination,
      weight: parseFloat(this.weight),
      price: parseFloat(this.price)
    };

    console.log(json(parcel));

    this.parcelService.post(parcel).then(() => {
      this.router.navigateToRoute('home');
    }).catch(error => {
      Utils.getErrors(error).then(errors => {
        console.log(errors);

        this.errorTitle = Object.keys(errors)[0];
        this.errorDetails = Object.values(errors)[0][0] as string;
      });
    });
  }
}
