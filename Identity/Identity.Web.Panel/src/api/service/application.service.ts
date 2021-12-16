import { AxiosInstance } from "axios";
import { messages } from "@/constants";
import IApplicationRule from "../rules/application.rule";
import { CreateApp, UpdateApp } from "../models/application.model";
import { Application } from "../models/account.model";


export default class ApplicationService implements IApplicationRule {

    private readonly _axois: AxiosInstance;

    constructor(axios: AxiosInstance) {
        this._axois = axios;
    }

    async GetUsers(app: Application) {
        try {
            let request = await this._axois.post("Application/GetUsers", app)
            return await request.data
        } catch (e) {
            return messages.netWorkError(e.message)
        }
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

    async Update(app: UpdateApp) {
        try {
            let request = await this._axois.post("Application/Update", app)
            return await request.data
        } catch (e) {
            return messages.netWorkError(e.message)
        }
    }

}