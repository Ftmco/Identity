import { CreateApp, UpdateApp } from "../models/application.model";

export default interface IApplicationRule {
    GetApplications(page: number, count: number): any;
    Delete(id: any): any;
    Create(app: CreateApp): any;
    Update(app: UpdateApp): any;
}