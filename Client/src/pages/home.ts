import { IShipment } from 'domain/IShipment';
import { LogManager, autoinject } from "aurelia-framework";
import { Router } from 'aurelia-router';
import 'bootstrap';
import { Collapse } from 'bootstrap';
import { ShipmentService } from 'services/shipment-service';

export var log = LogManager.getLogger('app.home');

@autoinject
export class Home {
  shipments: IShipment[] | undefined;
  error: string;

  constructor(
    private shipmentService: ShipmentService,
    private router: Router
  ) {
    this.loadShipments();
  }

  loadShipments() {
    this.shipmentService.getAllShipments().then(result => {
      this.shipments = result;

      console.log(this.shipments);

      // make dates UTC ISO8601
      this.shipments.forEach(shipment => {
        shipment.flightDate = new Date(shipment.flightDate + "Z");
      });
    }).catch(error => {
      this.error = error.name;
      console.error(error);
    });
  }

  finalize(shipment: IShipment) {
    this.shipmentService.finalize(shipment.number).then(_ => this.loadShipments());
  }

  toggleBagList(shipment: IShipment) {
    shipment.showOrders = true;

    let shipmentWrapper = document.querySelector(`[data-bag-list="${shipment.number}"]`);
    if (shipmentWrapper)
      new Collapse(shipmentWrapper, { parent: "#accordion" }).toggle();
  }
}
