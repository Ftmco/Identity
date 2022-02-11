import IProfileRules from "../rules/profile.rules";
import { Application } from "../models/application.model";
import { UpdateProfile } from "../models/profile.model";
export default class ProfileService implements IProfileRules {
    private readonly _axios;
    constructor();
    getProfile(app: Application): Promise<any>;
    updateProfile(profile: UpdateProfile): Promise<any>;
}
