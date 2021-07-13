import { IWeighedPriced } from './base/IWeighedPriced';
import { INumbered } from "./base/INumbered";

export interface IParcel extends INumbered, IWeighedPriced {
  bagNumber: string;
  recipient: string;
  destination: string;
}
