import { IBag } from 'domain/IBag';
import { ILetterBag } from 'domain/ILetterBag';
import { BagType } from 'domain/enums/BagType';
import { Router } from 'aurelia-router';
import { LogManager, autoinject } from "aurelia-framework";
import { ShipmentService } from 'services/shipment-service';
import { BagService } from 'services/bag-service';
import { json } from 'aurelia-fetch-client';
import { IShipment } from 'domain/IShipment';
import { Utils } from 'helpers/utils';

export var log = LogManager.getLogger('app.bag.edit');

@autoinject
export class Edit {
  shipments: IShipment[] | undefined;

  parcelNumber: string;
  parcelShipmentNumber: string;

  letterNumber: string;
  letterShipmentNumber: string;
  letterCount: string;
  letterWeight: string;
  letterPrice: string;

  errorTitle: string;
  errorDetails: string;

  constructor(
    private shipmentService: ShipmentService,
    private bagService: BagService,
    private router: Router
  ) {
    this.loadShipments();
  }

  activate(parameters: { number: string; }) {
    if (!parameters.number)
      return;

    this.populateInputFields(parameters.number);
  }

  loadShipments() {
    this.errorTitle = undefined;
    this.errorDetails = undefined;

    this.shipmentService.fetchAll<IShipment>().then(result => {
      this.shipments = result;

      console.log(this.shipments);

      // make dates UTC ISO8601
      this.shipments.forEach(shipment => {
        shipment.flightDate = new Date(shipment.flightDate);
      });
    }).catch(error => {
      Utils.getErrors(error).then(errors => {
        console.log(errors);

        this.errorTitle = Object.keys(errors)[0];
        this.errorDetails = Object.values(errors)[0][0] as string;
      });
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
    let bag = {
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
    this.errorTitle = undefined;
    this.errorDetails = undefined;

    console.log(json(bag));

    this.bagService.put(bag).then(_ => {
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
    this.bagService.fetch<IBag>(number).then(result => {
      if (this.instanceOfLetterBag(result)) {
        this.letterNumber = result.number;
        this.letterShipmentNumber = result.shipmentNumber;
        this.letterCount = result.letterCount.toString();
        this.letterWeight = result.weight.toString();
        this.letterPrice = result.price.toString();
      } else {
        this.parcelNumber = result.number;
        this.parcelShipmentNumber = result.shipmentNumber;
      }
    });
  }

  instanceOfLetterBag(object: any): object is ILetterBag {
    return 'letterCount' in object && object.letterCount != null;
  }
}
