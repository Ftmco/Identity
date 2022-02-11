"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.messages = exports.apiUrls = void 0;
exports.apiUrls = {
    login: "Account/Login",
    signUp: "Account/SignUp",
    getProfile: "Profile/GetProfile",
    updateProfile: "Profile/UpdateProfile"
};
exports.messages = {
    serverError: function (message) { return ({
        status: false,
        code: 500,
        title: "Server Error",
        message: message,
        resullt: {}
    }); }
};
