import { AxiosInstance } from "axios";
import { apiUrls, messages } from "../../constants";
import { addHeader, axios } from "../apiCall";
import { Login, Signup, ChangePassword, ResetPassword } from "../models/account.model";
import IAccountRules from "../rules/account.rules";

export default class AccountService implements IAccountRules {

    private readonly _axios: AxiosInstance;

    constructor() {
        this._axios = axios;
    }

    async forgotPassword(userName: string) {
        try {
            let request = await this._axios.post("Account/ForgotPassword", { userName: userName })
            return await request.data;
        } catch (e) {
            return messages.serverError(e.mssage)
        }
    }

    async resetPassword(reset: ResetPassword) {
        try {
            let request = await this._axios.post("Account/ResetPassword", reset)
            return await request.data
        } catch (e) {
            return messages.serverError(e.message)
        }
    }

    async login(login: Login) {
        try {
            let request = await this._axios.post(apiUrls.login, login)
            let response = await request.data
            if (response)
                addHeader(response.result.key, response.result.value)
            return response
        } catch (e) {
            return messages.serverError(e.message)
        }
    }

    async signUp(signup: Signup) {
        try {
            let request = await this._axios.post("Account/SignUp", signup)
            return await request.data
        } catch (e) {
            return messages.serverError(e.message)
        }
    }

    async changePassword(changePass: ChangePassword) {
        try {
            let request = await this._axios.post("Account/ChangePassword", changePass)
            return await request.data;
        } catch (e) {
            return messages.serverError(e.message)
        }
    }

}