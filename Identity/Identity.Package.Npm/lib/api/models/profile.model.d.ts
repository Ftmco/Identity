import { Application } from "./application.model";
export declare type UpdateProfile = {
    application: Application;
    image: ImageBase64;
    json: string;
    user: User;
};
export declare type User = {
    id: string;
    userName: string;
    fullName: string;
    email: string;
    mobileNo: string;
};
declare type ImageBase64 = {
    base64: string;
    extension: string;
    path?: null;
};
export {};
