import { AxiosInstance } from "axios";
import { apiUrls, messages } from "../../constants";
import { addHeader, axios, changeBaseURL } from "../apiCall";
import { Login, Signup, ChangePassword, ResetPassword } from "../models/account.model";
import IAccountRules from "../rules/account.rules";

export default class AccountService implements IAccountRules {

    private readonly _axios: AxiosInstance;

    constructor(baseUrl: string) {
        this._axios = axios;
        changeBaseURL(baseUrl)
    }

    forgotPassword(userName: string) {
        throw new Error("Method not implemented.");
    }

    resetPassword(reset: ResetPassword) {
        throw new Error("Method not implemented.");
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

    signUp(signup: Signup) {
        throw new Error("Method not implemented.");
    }

    changePassword(changePass: ChangePassword) {
        throw new Error("Method not implemented.");
    }

}