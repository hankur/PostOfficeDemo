import { IBag } from './../domain/IBag';
import { LogManager, autoinject } from "aurelia-framework";
import { HttpClient } from "aurelia-fetch-client";
import { BaseService } from './base-service';

export var log = LogManager.getLogger('app.service.bag');

@autoinject
export class BagService extends BaseService {
  constructor(
    private httpClient: HttpClient,
    private endPoint = 'Bag'
  ) {
    super(httpClient, endPoint);
  }
}
