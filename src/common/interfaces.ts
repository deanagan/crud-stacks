export interface Role {
    id: number;
    name: string;
    description: string;
}

export interface User {
    id: number;
    name: string;
    roleId: number;
    role: Role;
    email: string;
    hash: string;
}
