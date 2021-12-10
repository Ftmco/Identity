import { AxiosInstance } from "axios";
import { messages } from "../../constants";
import IApplicationRule from "../rules/application.rule";


export default class ApplicationService implements IApplicationRule {

    private readonly _axois: AxiosInstance;

    constructor(axios: AxiosInstance) {
        this._axois = axios;
    }

    async GetApplications(page: number, count: number) {
        try {
            let request = await this._axois.get(`Application/GetApplications?page=${page}&count=${count}`)
            return await request.data
        } catch (e) {
            return messages.netWorkError(e.message)
        }
    }

}