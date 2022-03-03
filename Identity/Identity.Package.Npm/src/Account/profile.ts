import axios from "axios";
import { createEncModel, decryptResponse } from "../api/enc/model";
import { apiUrls, messages } from "../constants";
import { Application } from "../models/application.model";
import { UpdateProfile } from "../models/profile.model";


export const getProfile = (application: Application) => {
    return new Promise(async (resolve, reject) => {
        try {
            const encModel = createEncModel(application, apiUrls.getProfile)
            const request = await axios.post(apiUrls.baseURL + apiUrls.getProfile, encModel)
            const response = decryptResponse(await request.data, apiUrls.getProfile)
            resolve(response)
        } catch (e) {
            reject(messages.serverError(e.message))
        }
    })
}

export const updateProfile = (updateProfile: UpdateProfile) => {
    return new Promise(async (resolve, reject) => {
        try {
            const encModel = createEncModel(updateProfile, apiUrls.updateProfile)
            const request = await axios.post(apiUrls.baseURL + apiUrls.updateProfile, encModel)
            const response = decryptResponse(await request.data, apiUrls.updateProfile)
            resolve(response)
        } catch (e) {
            reject(messages.serverError(e.message))
        }
    })
}