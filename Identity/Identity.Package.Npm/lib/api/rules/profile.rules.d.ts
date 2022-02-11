import { Application } from "../models/application.model";
import { UpdateProfile } from "../models/profile.model";
export default interface IProfileRules {
    getProfile(app: Application): any;
    updateProfile(profile: UpdateProfile): any;
}
