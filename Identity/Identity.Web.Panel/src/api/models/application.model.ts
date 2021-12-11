export type CreateApp = {
    name: string;
    password: string;
    file: File
}

export type UpdateApp = {
    id: string;
    name: string;
    password: string;
    file: File
}


export type File = {
    base64: string;
    extension: string;
}