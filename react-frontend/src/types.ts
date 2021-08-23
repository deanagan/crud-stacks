
export interface Repair {
    id?: number;
    name: string;
    detail: string;
    fixed: boolean;
}

export interface RepairState {
    readonly loading: boolean;
    readonly repairs: Repair[];
    readonly errors?: string;
}

export interface IHttpClientRequestParameters<T> {
    url: string
    requiresToken: boolean
    payload?: T
}

export interface IHttpClient {
    get<T>(parameters: IHttpClientRequestParameters<T>): Promise<T>
    post<T>(parameters: IHttpClientRequestParameters<T>): Promise<T>
}
