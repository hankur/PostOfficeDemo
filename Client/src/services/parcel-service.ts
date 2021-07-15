import { IParcel } from 'domain/IParcel';
import { LogManager, autoinject } from "aurelia-framework";
import { HttpClient } from "aurelia-fetch-client";
import { BaseService } from './base-service';

export var log = LogManager.getLogger('app.service.parcel');

@autoinject
export class ParcelService extends BaseService {
  constructor(
    private httpClient: HttpClient,
    private endPoint = 'Parcel'
  ) {
    super(httpClient, endPoint);
  }

  getParcel(number: string): Promise<IParcel> {
    return super.fetch(`/Number/${number}`);
  }

  createParcel(parcel: IParcel): Promise<Response> {
    return super.post(parcel);
  }

  updateParcel(parcel: IParcel): Promise<Response> {
    return super.put(parcel);
  }
}
