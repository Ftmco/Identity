import * as models from "../models/account.model"

/**
 * Account Rules 
 */
export default interface IAccountRules {
    login(login: models.Login): any;
    signUp(signup: models.Signup): any;
    changePassword(changePass: models.ChangePassword): any;
    forgotPassword(userName: string): any;
    resetPassword(reset: models.ResetPassword): any;
}