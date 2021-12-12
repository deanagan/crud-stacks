import { v4 as uuidv4 } from "uuid";
import { NIL as NIL_UUID } from 'uuid';


export type uuidv4Type = typeof uuidv4;
export const emptyGuid = NIL_UUID;

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
    assignee?: Assignee | null;
}

export interface User {
    uniqueId?: uuidv4Type;
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
}

export interface LoginEntryForm {
    email: string;
    password: string;
}

export interface SignUpForm {
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    roleUniqueId: uuidv4Type;
}

export interface AuthLoggedInUser {
    userName: string;
    email: string;
    role: string;
    token: string;
}

export interface AuthState {
    readonly loading: boolean;
    readonly currentLoggedInUser?: AuthLoggedInUser;
    readonly error?: string;
}

export interface TodoState {
    readonly loading: boolean;
    readonly todos: Todo[];
    readonly errors?: string;
}

export interface UserState {
    readonly loading: boolean;
    readonly users: User[];
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
