import { AxiosInstance } from "axios";
import { apiUrls } from "../../constants";
import { axios } from "../apiCall";
import { Login, Signup, ChangePassword, ResetPassword } from "../models/account.model";
import IAccountRules from "../rules/account.rules";

export default class AccountService implements IAccountRules {

    private readonly _axios: AxiosInstance;

    constructor() {
        this._axios = axios;
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
            return await request.data;
        } catch (e) {
            return {};
        }
    }

    signUp(signup: Signup) {
        throw new Error("Method not implemented.");
    }

    changePassword(changePass: ChangePassword) {
        throw new Error("Method not implemented.");
    }

}