import { Application } from "./application.model";
/**
 * Login model for login user into application
 */
export declare type Login = {
    userName: string;
    password: string;
    application: Application;
};
export declare type Signup = {
    userName: string;
    email: string;
    fullName: string;
    mobileNo: string;
    password: string;
    application: Application;
};
export declare type ChangePassword = {
    currentPassword: string;
    newPassword: string;
};
export declare type ResetPassword = {
    userName: string;
    code: string;
    password: string;
};
