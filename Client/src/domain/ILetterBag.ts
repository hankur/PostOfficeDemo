import { IWeighedPriced } from "./base/IWeighedPriced";
import { IBag } from "./IBag";

export interface ILetterBag extends IBag, IWeighedPriced {
  letterCount: number;
}
