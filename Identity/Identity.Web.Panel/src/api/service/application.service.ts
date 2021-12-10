import { AxiosInstance } from "axios";
import { messages } from "@/constants";
import IApplicationRule from "../rules/application.rule";
import { CreateApp } from "../models/application.model";


export default class ApplicationService implements IApplicationRule {

    private readonly _axois: AxiosInstance;

    constructor(axios: AxiosInstance) {
        this._axois = axios;
    }

    async Create(app: CreateApp) {
        try {
            let request = await this._axois.post("Application/Create", app)
            return await request.data
        } catch (e) {
            return messages.netWorkError(e.message)
        }
    }

    async GetApplications(page: number, count: number) {
        try {
            let request = await this._axois.get(`Application/GetApplications?page=${page}&count=${count}`)
            return await request.data
        } catch (e) {
            return messages.netWorkError(e.message)
        }
    }

    async Delete(id: any) {
        try {
            let request = await this._axois.post("Application/Delete", { id: id })
            return await request.data
        } catch (e) {
            return messages.netWorkError(e.message)
        }
    }

}