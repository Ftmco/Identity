export type Login = {
    userName: string;
    password: string;
    application: Application
}

export type SignUp = {
    userName: string;
    fullName: string;
    password: string;
    email: string;
    mobileNo: string;
    application: Application
}

export type Application = {
    apikey: string;
    password: string;
}