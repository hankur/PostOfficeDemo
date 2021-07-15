import { IShipment } from './../../domain/IShipment';
import { Router } from 'aurelia-router';
import { LogManager, autoinject } from "aurelia-framework";
import { ShipmentService } from 'services/shipment-service';
import { json } from 'aurelia-fetch-client';
import { Utils } from 'helpers/utils';

export var log = LogManager.getLogger('app.shipment.edit');

@autoinject
export class Edit {
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

  activate(parameters: { number: string; }) {
    if (!parameters.number)
      return;

    this.populateInputFields(parameters.number);
  }

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

    this.shipmentService.put(shipment).then(() => {
      this.router.navigateToRoute('home');
    }).catch(error => {
      Utils.getErrors(error).then(errors => {
        console.log(errors);

        this.errorTitle = Object.keys(errors)[0];
        this.errorDetails = Object.values(errors)[0][0] as string;
      });
    });
  }

  populateInputFields(number: string) {
    this.shipmentService.fetch<IShipment>(number).then(result => {
      this.number = result.number;
      this.airport = result.airport;
      this.flightNumber = result.flightNumber;
      this.flightDate = result.flightDate.toString();
    });
  }
}
