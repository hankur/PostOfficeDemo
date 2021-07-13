import { Airport } from 'domain/enums/Airport';
import { Router } from 'aurelia-router';
import { LogManager, autoinject } from "aurelia-framework";
import { ShipmentService } from 'services/shipment-service';
import { json } from 'aurelia-fetch-client';

export var log = LogManager.getLogger('app.shipment.index');

@autoinject
export class Index {
  number: string;
  airport: string;
  flightNumber: string;
  flightDate: string;

  constructor(
    private shipmentService: ShipmentService,
    private router: Router
  ) { }

  submit() {
    let shipment = {
      number: this.number,
      airport: this.airport,
      flightNumber: this.flightNumber,
      flightDate: this.flightDate
    };

    console.log(json(shipment));

    this.shipmentService.createShipment(shipment).then(() => {
      this.router.navigateToRoute('home');
    }).catch(error => console.error(error));
  }
}
