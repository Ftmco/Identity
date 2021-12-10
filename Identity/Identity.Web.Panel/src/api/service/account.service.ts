import { AxiosInstance } from "axios";
import { messages } from "@/constants";
import { Login } from "../models/account.model";
import IAccountRule from "../rules/account.rule";
import { changeConfigHeader } from "..";

export default class AccountServiec implements IAccountRule {

    private readonly _axios: AxiosInstance;

    constructor(axios: AxiosInstance) {
        this._axios = axios;
    }

    async Login(login: Login) {
        try {
            let request = await this._axios.post("Account/Login", login)
            let response = await request.data
            if (response.status) {
                localStorage.setItem(response.result.session.key, response.result.session.value)
                changeConfigHeader(response.result.session.key, response.result.session.value)
            }
            return response
        }
        catch(e) {
            return messages.netWorkError(e.message);
        }
    }

    async LogOut() {
        try {
            let request = await this._axios.get("Account/Logout")
            let response = await request.data
            if (response.status) {
                localStorage.removeItem("I-Authentication")
            }
            return response
        } catch (e) {
            return messages.netWorkError(e.message);
        }
    }
}