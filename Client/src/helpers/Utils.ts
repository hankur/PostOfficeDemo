import { ServiceResponseError } from "./ServiceResponseError";

export module Utils {
  export async function getErrors(error: ServiceResponseError): Promise<any> {
    let generic = { "Error": "Something went wrong, please try again later" };
    if (!(error instanceof ServiceResponseError))
      return generic;

    let body = await error.getBody();
    return 'errors' in body ? body.errors : body;
  }
}
