export interface User {
    userId: number;
    active: boolean;
    username: string;
    password?: string;
    role: string;
    token: string;
}
