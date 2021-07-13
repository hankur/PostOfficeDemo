import { BagType } from './enums/BagType';
import { INumbered } from "./base/INumbered";

export interface IBag extends INumbered {
  type: BagType;
  shipmentNumber: string;
}
