import { IWeighedPriced } from './base/IWeighedPriced';
import { BagType } from './enums/BagType';
import { INumbered } from "./base/INumbered";

export interface IBag extends INumbered, IWeighedPriced {
  type: BagType;
  typeName: string;
  shipmentNumber: string;
}
