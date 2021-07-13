export class ServiceResponseError implements Error {
  name: string;
  message: string;
  body: any;

  constructor(
    public response: Response
  ) {
    this.name = `${response.status}`;
    this.message = response.statusText;
  }

  getBody() {
    return this.response.json();
  }
}
