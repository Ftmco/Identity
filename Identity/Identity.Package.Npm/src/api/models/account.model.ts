import { Application } from "./application.model"

export type Login = {
    userName: string;
    password: string;
    application: Application
}

export type Signup = {
    userName: string;
    email: string;
    fullName: string;
    mobileNo: string;
    password: string;
    application: Application
}

export type ChangePassword = {
    currentPassword: string;
    newPassword: string;
}

export type ResetPassword = {
    userName: string;
    code: string;
    password: string;
}