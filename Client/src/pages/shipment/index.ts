import { Router } from 'aurelia-router';
import { LogManager, autoinject } from "aurelia-framework";
import { ShipmentService } from 'services/shipment-service';
import { json } from 'aurelia-fetch-client';
import { Utils } from 'helpers/Utils';

export var log = LogManager.getLogger('app.shipment.index');

@autoinject
export class Index {
  number: string;
  airport: string;
  flightNumber: string;
  flightDate: string;

  errorTitle: string;
  errorDetails: string;

  constructor(
    private shipmentService: ShipmentService,
    private router: Router
  ) { }

  submit() {
    this.errorTitle = undefined;
    this.errorDetails = undefined;

    let shipment = {
      number: this.number,
      airport: this.airport,
      flightNumber: this.flightNumber,
      flightDate: this.flightDate
    };

    console.log(json(shipment));

    this.shipmentService.post(shipment).then(() => {
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
