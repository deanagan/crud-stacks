
export interface Task {
    id?: number;
    name: string;
    detail: string;
    fixed: boolean;
}

export interface TaskState {
    readonly loading: boolean;
    readonly tasks: Task[];
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
