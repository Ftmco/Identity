import { AxiosInstance } from "axios";
import { messages } from "@/constants";
import { Login, SignUp, Application } from "../models/account.model";
import IAccountRule from "../rules/account.rule";
import { changeConfigHeader } from "..";

export default class AccountServiec implements IAccountRule {

    private readonly _axios: AxiosInstance;

    private readonly application: Application = {
        apikey: "54AD86E7-BC7B-4B24-A43A-4AD0ADD6EBAF",
        password: "1G14ijWA"
    }

    constructor(axios: AxiosInstance) {
        this._axios = axios;
    }

    async SignUp(signUp: SignUp) {
        try {
            signUp.application = this.application
            let request = await this._axios.post("Account/SignUp", signUp)
            return await request.data;
        } catch (e) {
            return messages.netWorkError(e.message);
        }
    }

    async Login(login: Login) {
        try {
            login.application = this.application;
            let request = await this._axios.post("Account/Login", login)
            let response = await request.data
            if (response.status) {
                localStorage.setItem(response.result.key, response.result.value)
                changeConfigHeader(response.result.key, response.result.value)
            }
            return response
        }
        catch (e) {
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