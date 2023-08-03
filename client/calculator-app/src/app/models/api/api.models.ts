export interface IFetchProps {
  url: string;
  method: string;
  headers?: HeadersInit;
  body?: BodyInit | null;
}

export interface ICustomResponse {
  readonly ok: boolean;
  readonly redirected: boolean;
  readonly status: number;
  readonly statusText: string;
  readonly type: ResponseType;
  data: any;
}
