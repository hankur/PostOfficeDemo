import { bindable } from 'aurelia-framework';

export class LoadingCustomElement {
  @bindable error: boolean;
  @bindable errorTitle: string;
  @bindable errorDetails: string;
  @bindable errorDetailsClass: string;

  @bindable notFound: boolean;
  @bindable notFoundTitle: string;
  @bindable notFoundDetails: string;
  @bindable notFoundDetailsClass: string;
}
