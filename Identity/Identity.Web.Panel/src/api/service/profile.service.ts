import { messages } from "@/constants";
import { AxiosInstance } from "axios";
import { Application } from "../models/account.model";
import { Profile } from "../models/profile.model";
import IProfileRules from "../rules/profile.rules";

export default class ProfileService implements IProfileRules {

    private readonly _axios: AxiosInstance;

    private readonly application: Application = {
        apikey: "54AD86E7-BC7B-4B24-A43A-4AD0ADD6EBAF",
        password: "1G14ijWA"
    }

    constructor(axios: AxiosInstance) {
        this._axios = axios;
    }

    async getProfile() {
        try {
            let request = await this._axios.post("Profile/GetProfile", this.application)
            let response = await request.data;
            if (response.status)
                response.result.json = JSON.parse(response.result.json)
            return response
        } catch (e) {
            return messages.netWorkError(e.message)
        }
    }

    async updateProfile(profile: Profile) {
        try {
            profile.application = this.application
            let request = await this._axios.post("Profile/GetProfile", profile)
            let response = await request.data;
            if (response.status)
                response.result.json = JSON.parse(response.result.json)
            return response
        } catch (e) {
            return messages.netWorkError(e.message)
        }
    }

}