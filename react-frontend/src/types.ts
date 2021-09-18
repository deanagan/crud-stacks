import { v4 as uuid } from "uuid";

export type uuidv4 = typeof uuid;
export interface Todo {
    uniqueId?: uuidv4;
    summary: string;
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
