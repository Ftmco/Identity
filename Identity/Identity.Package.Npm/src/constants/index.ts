export const apiUrls = {
    login: "Account/Login",
    signUp: "Account/SignUp",
    getProfile: "Profile/GetProfile",
    updateProfile: "Profile/UpdateProfile"
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