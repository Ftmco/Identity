import { Login, Signup, ChangePassword, ResetPassword } from "../models/account.model";
import IAccountRules from "../rules/account.rules";
export default class AccountService implements IAccountRules {
    private readonly _axios;
    constructor();
    forgotPassword(userName: string): Promise<any>;
    resetPassword(reset: ResetPassword): Promise<any>;
    login(login: Login): Promise<any>;
    signUp(signup: Signup): Promise<any>;
    changePassword(changePass: ChangePassword): Promise<any>;
}
