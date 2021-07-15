import { LogManager, autoinject } from "aurelia-framework";
import { HttpClient } from "aurelia-fetch-client";
import { BaseService } from './base-service';
import { IShipment } from "domain/IShipment";

export var log = LogManager.getLogger('app.service.shipment');

@autoinject
export class ShipmentService extends BaseService {
  constructor(
    private httpClient: HttpClient,
    private endPoint = 'Shipment'
  ) {
    super(httpClient, endPoint);
  }

  getShipment(number: string): Promise<IShipment> {
    return super.fetch(`/Number/${number}`);
  }

  getAllShipments(): Promise<IShipment[]> {
    return super.fetchAll<IShipment>(`/List`);
  }

  finalize(number: string): Promise<Response> {
    return super.post(null, `/${number}/Finalize`);
  }

  createShipment(shipment: any): Promise<Response> {
    return super.post(shipment);
  }

  updateShipment(shipment: any): Promise<Response> {
    return super.put(shipment);
  }
}
