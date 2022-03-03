export const apiUrls = {
    baseURL: "",
    login: "/api/Account/Login",
    logOut: "/api/Account/LogOut",
    signUp: "/api/Account/SignUp",
    changePassword: "/api/Account/ChangePassword",
    forgotPassword: "/api/Account/ForgotPassword",
    resetPassword: "/api/Account/ResetPassword",
    getProfile: "/api/Profile/GetProfile",
    updateProfile: "/api/Profile/UpdateProfile"
}

export const changeBaseUrl = (url: string) => {
    apiUrls.baseURL = url
    return apiUrls.baseURL
}

export const messages = {
    serverError: (message: string) => ({
        status: false,
        code: 500,
        title: "Server Error",
        message,
        resullt: {}
    })
}