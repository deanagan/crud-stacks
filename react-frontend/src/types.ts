
export interface Todo {
    id?: number;
    name: string;
    detail: string;
    isDone: boolean;
}

export interface TodoState {
    readonly loading: boolean;
    readonly todos: Todo[];
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
