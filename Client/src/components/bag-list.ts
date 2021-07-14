import { IBag } from './../domain/IBag';
import { LogManager, autoinject, bindable } from 'aurelia-framework';
import 'bootstrap';
import { Collapse } from 'bootstrap';
import { ILetterBag } from 'domain/ILetterBag';

export var log = LogManager.getLogger('app.components.bag-list');

@autoinject
export class BagListCustomElement {
  @bindable bags: IBag[];

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
