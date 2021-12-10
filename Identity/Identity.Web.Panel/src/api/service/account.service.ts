import { AxiosInstance } from "axios";
import { messages } from "@/constants";
import { Login } from "../models/account.model";
import IAccountRule from "../rules/account.rule";

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
                localStorage.setItem(response.result.key, response.result.value)
            }
            return response
        }
        catch(e) {
            return messages.netWorkError(e.message);
        }
    }

}