import { Utils } from 'helpers/utils';
import { IParcelBag } from './../domain/IParcelBag';
import { IShipment } from 'domain/IShipment';
import { LogManager, autoinject } from "aurelia-framework";
import { Router } from 'aurelia-router';
import 'bootstrap';
import { Collapse } from 'bootstrap';
import { ShipmentService } from 'services/shipment-service';
import { BagType } from 'domain/enums/BagType';

export var log = LogManager.getLogger('app.home');

@autoinject
export class Home {
  shipments: IShipment[] | undefined;

  errorTitle: string;
  errorDetails: string;

  constructor(
    private shipmentService: ShipmentService,
    private router: Router
  ) {
    this.loadShipments();
  }

  loadShipments() {
    this.errorTitle = undefined;
    this.errorDetails = undefined;

    this.shipmentService.getAllShipments().then(result => {
      this.shipments = result;

      console.log(this.shipments);

      // make dates UTC ISO8601
      this.shipments.forEach(shipment => {
        shipment.flightDate = new Date(shipment.flightDate + "Z");

        shipment.bags.forEach(bag => {
          bag.typeName = BagType[bag.type];

          if (this.instanceOfParcelBag(bag)) {
            bag.parcels.forEach(parcel => {
              bag.price += parcel.price;
              bag.weight += parcel.weight;
            });
          }
        });
      });
    }).catch(error => {
      Utils.getErrors(error).then(errors => {
        console.log(errors);

        this.errorTitle = Object.keys(errors)[0];
        this.errorDetails = Object.values(errors)[0][0] as string;
      });
    });
  }

  finalize(shipment: IShipment) {
    this.shipmentService.finalize(shipment.number).then(_ => this.loadShipments());
  }

  toggleBagList(shipment: IShipment) {
    let bagListWrapper = document.querySelector(`[data-bag-list="${shipment.number}"]`);
    if (bagListWrapper)
      new Collapse(bagListWrapper, { parent: "#accordion" }).toggle();
  }

  instanceOfParcelBag(object: any): object is IParcelBag {
    return 'parcels' in object;
  }
}
