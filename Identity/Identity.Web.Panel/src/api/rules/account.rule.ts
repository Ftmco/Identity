import { Login } from "../models/account.model";

export default interface IAccountRule {
    Login(login: Login): any;
}