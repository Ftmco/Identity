import IProfileRules from "../rules/profile.rules";
import { Application } from "../models/application.model";
import { UpdateProfile } from "../models/profile.model";
import { AxiosInstance } from "axios";
import { axios } from "../apiCall";
import { apiUrls } from "../../constants";

export default class ProfileService implements IProfileRules {

    private readonly _axios: AxiosInstance;

    constructor() {
        this._axios = axios;
    }

    async getProfile(app: Application) {
        try {
            let request = await this._axios.post(apiUrls.getProfile, app)
            return await request.data
        } catch (e) {

        }
    }

    async updateProfile(profile: UpdateProfile) {
        try {
            let request = await this._axios.post(apiUrls.updateProfile, profile)
            return await request.data
        } catch (e) {

        }
    }
}