export declare const apiUrls: {
    login: string;
    signUp: string;
    getProfile: string;
    updateProfile: string;
};
export declare const messages: {
    serverError: (message: string) => {
        status: boolean;
        code: number;
        title: string;
        message: string;
        resullt: {};
    };
};
