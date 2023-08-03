import {ICustomResponse, IFetchProps} from "../models/api/api.models";
import {Injectable} from "@angular/core";

@Injectable()
abstract class BaseApiService {
  public response?: any;
  private _headers: HeadersInit = {
    "Content-Type": "application/json",
    'Accept': 'application/json'
  };

  fetch = async (props: IFetchProps) => {
    const headers: { [key: string]: any } = {...this._headers, ...props.headers};

    this.response = await fetch(props.url, {method: props.method, headers, body: props.body})
      .then((response: Response): Promise<ICustomResponse> => {
        return this.makeCustomResponse(response)
      });
  }

  private makeCustomResponse = async (response: Response): Promise<ICustomResponse> => {
    const {ok, redirected, status, statusText, type} = response;

    const customResponse: ICustomResponse = {
      ok,
      redirected,
      status,
      statusText,
      type,
      data: null,
    };
    await response.text().then((data: string) => {
      if (data) {
        try {
          customResponse.data = JSON.parse(data);
        } catch (e) {
        }
      }
    });
    return customResponse;
  };
}

export default BaseApiService;
