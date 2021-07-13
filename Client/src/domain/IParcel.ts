import { INumbered } from "./base/INumbered";

export interface IParcel extends INumbered {
  recipient: string;
  destination: string;
}
