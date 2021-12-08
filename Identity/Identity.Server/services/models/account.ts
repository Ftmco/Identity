
export type SignUp = {
    userName: string;
    fullName: string;
    email?: string;
    mobileNo?: string;
    password: string;
}

export type Login = {
    userName: string;
    password: string;
}