import { SignUp, Login } from "../models/account";
import accountRule from "../rule/account.rulle";

class accountService implements accountRule {

    signUp(signUp: SignUp) {
        throw new Error("Method not implemented.");
    }

    login(login: Login) {
        throw new Error("Method not implemented.");
    }
   
}