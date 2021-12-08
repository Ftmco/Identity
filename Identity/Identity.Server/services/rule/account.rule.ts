import * as models from "../models/account";

export default interface accountRule {
    signUp(signUp: models.SignUp);
    login(login: models.Login);
}