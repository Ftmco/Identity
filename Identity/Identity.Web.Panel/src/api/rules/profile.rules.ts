import { Profile } from "../models/profile.model";

export default interface IProfileRules {
    getProfile(): any;

    updateProfile(profile: Profile): any;
}