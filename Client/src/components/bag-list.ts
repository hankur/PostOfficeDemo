import { EventAggregator } from 'aurelia-event-aggregator';
import { BagService } from 'services/bag-service';
import { Router } from 'aurelia-router';
import { IBag } from 'domain/IBag';
import { LogManager, autoinject, bindable } from 'aurelia-framework';
import 'bootstrap';
import { Collapse } from 'bootstrap';
import { ILetterBag } from 'domain/ILetterBag';
import { Utils } from 'helpers/Utils';
import { PostOfficeEvent } from 'helpers/PostOfficeEvent';

export var log = LogManager.getLogger('app.components.bag-list');

@autoinject
export class BagListCustomElement {
  @bindable finalized: boolean;
  @bindable bags: IBag[];

  @bindable errorTitle: string;
  @bindable errorDetails: string;

  constructor(
    private router: Router,
    private bagService: BagService,
    private eventAggregator: EventAggregator
  ) { }

  editBag(bag: IBag) {
    this.router.navigateToRoute('bagEdit', { number: bag.number });
  }

  deleteBag(bag: IBag) {
    this.errorTitle = undefined;
    this.errorDetails = undefined;

    this.bagService.delete(bag.number)
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

  toggleParcelList(bag: IBag) {
    if (this.instanceOfLetterBag(bag))
      return;

    let parcelWrapper = document.querySelector(`[data-parcel-list="${bag.number}"]`);
    if (parcelWrapper)
      new Collapse(parcelWrapper, { parent: "#accordion-bags" }).toggle();
  }

  instanceOfLetterBag(object: any): object is ILetterBag {
    return 'letterCount' in object && object.letterCount != null;
  }
}
