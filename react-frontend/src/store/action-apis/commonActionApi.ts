import axios, { AxiosRequestConfig } from "axios";
import { IHttpClient, IHttpClientRequestParameters } from "../../types";

export class HttpClient implements IHttpClient {
  post<T>(parameters: IHttpClientRequestParameters<T>): Promise<T> {
    return new Promise<T>((resolve, reject) => {
      const { url, payload, requiresToken } = parameters;

      // axios request options like headers etc
      const options: AxiosRequestConfig = {
        headers: {},
      };

      // if API endpoint requires a token, we'll need to add a way to add this.
      if (requiresToken) {
    //     const token = this.getToken();
    //     options.headers.RequestVerificationToken = token;
      }

      // finally execute the GET request with axios:
      axios
        .post(url, payload, options)
        .then((response: any) => {
          resolve(response.data as T);
        })
        .catch((response: any) => {
          reject(response);
        });
    });
  }

  put<T>(parameters: IHttpClientRequestParameters<T>): Promise<T> {
    return new Promise<T>((resolve, reject) => {
      const { url, payload, requiresToken } = parameters;

      // axios request options like headers etc
      const options: AxiosRequestConfig = {
        headers: {},
      };

      // if API endpoint requires a token, we'll need to add a way to add this.
      if (requiresToken) {
    //     const token = this.getToken();
    //     options.headers.RequestVerificationToken = token;
      }

      // finally execute the GET request with axios:
      axios
        .put(url, {...payload}, options)
        .then((response: any) => {
          resolve(response.data as T);
        })
        .catch((response: any) => {
          reject(response);
        });
    });
  }

  delete<T>(parameters: IHttpClientRequestParameters<T>): Promise<T> {
    return new Promise<T>((resolve, reject) => {
      const { url, requiresToken } = parameters;

      // axios request options like headers etc
      const options: AxiosRequestConfig = {
        headers: {},
      };

      // if API endpoint requires a token, we'll need to add a way to add this.
      if (requiresToken) {
    //     const token = this.getToken();
    //     options.headers.RequestVerificationToken = token;
      }

      // finally execute the GET request with axios:
      axios
        .delete(url, options)
        .then((response: any) => {
          resolve(response.data as T);
        })
        .catch((response: any) => {
          reject(response);
        });
    });
  }

  get<T>(parameters: IHttpClientRequestParameters<T>): Promise<T> {
    return new Promise<T>((resolve, reject) => {
      // extract the individual parameters
      const { url, requiresToken } = parameters;

      // axios request options like headers etc
      const options: AxiosRequestConfig = {
        headers: {},
      };

      // if API endpoint requires a token, we'll need to add a way to add this.
      if (requiresToken) {
        // const token = this.getToken();
        // options.headers.RequestVerificationToken = token;
      }

      // finally execute the GET request with axios:
      axios
        .get(url, options)
        .then((response: any) => {
          resolve(response.data as T);
        })
        .catch((response: any) => {
          reject(response);
        });
    });
  }
}
