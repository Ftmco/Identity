import { Application } from "./application.model"

export type UpdateProfile = {
    application: Application,
    image: ImageBase64,
    json: string,
    user: User
}

export type User = {
    id: string;
    userName: string;
    fullName: string;
    email: string;
    mobileNo: string;
}

type ImageBase64 = {
    base64: string;
    extension: string;
    path?: null;
}