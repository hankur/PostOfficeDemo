import { ServiceResponseError } from '../helpers/ServiceResponseError';
import { LogManager } from "aurelia-framework";
import { HttpClient, json } from 'aurelia-fetch-client';
import * as environment from '../../config/environment.json';

export var log = LogManager.getLogger('app.services.base');

export class BaseService {
  serviceHttpClient: HttpClient;
  serviceEndpoint: string;

  constructor(
    httpClient: HttpClient,
    endPoint: string,
  ) {
    this.serviceHttpClient = httpClient.configure(config => {
      // We can only configure stuff that applies to every service, logged in or not
      config
        .withBaseUrl(environment.api)
        .withDefaults({
          headers: {
            cache: 'no-store'
          }
        });
    });

    this.serviceEndpoint = endPoint;
  }

  fetchOne<TEntity>(number: string, finalEndpoint: string = ""): Promise<TEntity> {
    if (finalEndpoint == "")
      finalEndpoint = `/Number/${number}`;

    return this.fetch<TEntity>(finalEndpoint);
  }

  fetchAll<TEntity>(finalEndpoint: string = ""): Promise<TEntity[]> {
    if (finalEndpoint == "")
      finalEndpoint = `/List`;

    return this.fetch<TEntity[]>(finalEndpoint);
  }

  fetch<TEntity>(finalEndpoint: string = ""): Promise<TEntity> {
    let url = `${this.serviceEndpoint}${finalEndpoint}`;
    return new Promise((res, rej) => this.serviceHttpClient.fetch(url)
      .then(this._res).then(json => this._suc(json, res)).catch(reason => this._err(reason, rej)));
  }


  post<TEntity>(entity: any, finalEndpoint: string = ""): Promise<TEntity> {
    let url = `${this.serviceEndpoint}${finalEndpoint}`;
    return new Promise((res, rej) => this.serviceHttpClient.post(url, json(entity))
      .then(this._res).then(json => this._suc(json, res)).catch(reason => this._err(reason, rej)));
  }


  put(data: any = null, finalEndpoint: string = ""): Promise<Response> {
    let url = `${this.serviceEndpoint}${finalEndpoint}`;
    return new Promise((res, rej) => this.serviceHttpClient.put(url, json(data))
      .then(this._res).then(json => this._suc(json, res)).catch(reason => this._err(reason, rej)));
  }


  delete(number: string, finalEndpoint: string = ""): Promise<Response> {
    if (finalEndpoint == "")
      finalEndpoint = `/Number/${number}`;

    let url = `${this.serviceEndpoint}${finalEndpoint}`;
    return new Promise((res, rej) => this.serviceHttpClient.delete(url, null)
      .then(this._res).then(json => this._suc(json, res)).catch(reason => this._err(reason, rej)));
  }


  private _res(response: Response) {
    if (response.status >= 200 && response.status < 300) {
      if (response.status != 204)
        return response.json();
    } else {
      throw new ServiceResponseError(response);
    }
  }

  private _suc(json: string, res: Function) {
    res(json);
  }

  private _err(reason: Error, rej: Function) {
    rej(reason);
  }
}
