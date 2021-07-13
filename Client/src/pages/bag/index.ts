import { ILetterBag } from './../../domain/ILetterBag';
import { BagType } from 'domain/enums/BagType';
import { Router } from 'aurelia-router';
import { LogManager, autoinject } from "aurelia-framework";
import { ShipmentService } from 'services/shipment-service';
import { BagService } from 'services/bag-service';
import { json } from 'aurelia-fetch-client';
import { IShipment } from 'domain/IShipment';

export var log = LogManager.getLogger('app.bag.index');

@autoinject
export class Index {
  shipments: IShipment[] | undefined;

  parcelNumber: string;
  parcelShipmentNumber: string;

  letterNumber: string;
  letterShipmentNumber: string;
  letterCount: string;
  letterWeight: string;
  letterPrice: string;

  constructor(
    private shipmentService: ShipmentService,
    private bagService: BagService,
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
      console.error(error);
    });
  }

  submitParcelBag() {
    let bag = {
      number: this.parcelNumber,
      shipmentNumber: this.parcelShipmentNumber,
      type: BagType.Parcels
    };

    this.submitBag(bag);
  }

  submitLetterBag() {
    let bag: ILetterBag = {
      number: this.letterNumber,
      shipmentNumber: this.letterShipmentNumber,
      letterCount: parseInt(this.letterCount),
      weight: parseFloat(this.letterWeight),
      price: parseFloat(this.letterPrice),
      type: BagType.Letters
    };

    this.submitBag(bag);
  }

  submitBag(bag: any) {
    console.log(json(bag));

    this.bagService.createBag(bag).then(() => {
      this.router.navigateToRoute('home');
    }).catch(error => console.error(error));
  }
}
