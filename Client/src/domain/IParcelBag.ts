import { IBag } from "./IBag";
import { IParcel } from "./IParcel";

export interface IParcelBag extends IBag {
  parcels: IParcel[];
}
