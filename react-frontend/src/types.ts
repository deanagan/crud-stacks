import { v4 as uuidv4 } from "uuid";

export type uuidv4Type = typeof uuidv4;
export const newUuidV4 = () => uuidv4();

export interface Assignee {
    uniqueId: uuidv4Type;
    firstName: string;
    lastName: string;
}
export interface Todo {
    uniqueId?: uuidv4Type;
    summary: string;
    detail: string;
    isDone: boolean;
    assignee?: Assignee;
}

export interface User {
    uniqueId?: uuidv4Type;
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
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
