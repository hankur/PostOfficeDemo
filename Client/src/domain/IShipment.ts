import { Airport } from "./enums/Airport";
import { INumbered } from "./base/INumbered";

export interface IShipment extends INumbered {
  airport: Airport;

  flightNumber: string;
  flightDate: Date | string;

  bags: INumbered[];
  finalized: boolean;

  showOrders: boolean | undefined;
}
