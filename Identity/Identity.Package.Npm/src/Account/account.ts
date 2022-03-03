import { axios, addHeader } from "../api/apiCall";
import { ChangePassword, Login, ResetPassword, Signup } from "../models/account.model";
import { apiUrls, messages } from "../constants";
import { createEncModel, decryptResponse } from "../api/enc/model";

export const login = (login: Login) => {
    return new Promise(async (resolve, reject) => {
        try {
            const encModel = createEncModel(login, apiUrls.login)
            const request = await axios.post(apiUrls.baseURL + apiUrls.login, encModel)
            const response = decryptResponse(await request.data, apiUrls.login)
            if (response.Status) {
                addHeader(response.Result.Key, response.Result.Value)
                resolve(response)
            } else {
                reject(response)
            }
        }
        catch (e) {
            reject(messages.serverError(e.message))
        }
    })
}

export const register = (signup: Signup) => {
    return new Promise(async (resolve, reject) => {
        try {
            const request = await axios.post(apiUrls.baseURL + apiUrls.signUp, signup)
            const response = await request.data
            if (response.status) {
                resolve(response)
            } else {
                reject(response)
            }
        }
        catch (e) {
            reject(messages.serverError(e.message))
        }
    })
}

export const logout = () => {
    return new Promise(async (resolve, reject) => {
        try {
            const request = await axios.post(apiUrls.baseURL + apiUrls.logOut)
            const response = await request.data
            resolve(response)
        }
        catch (e) {
            reject(messages.serverError(e.message))
        }
    })
}

export const changePassword = (changePassword: ChangePassword) => {
    return new Promise(async (resolve, reject) => {
        try {
            const request = await axios.post(apiUrls.baseURL + apiUrls.changePassword, changePassword)
            const response = await request.data
            resolve(response)
        } catch (e) {
            reject(messages.serverError(e.message))
        }
    })
}

export const forgotPassword = (userName: string) => {
    return new Promise(async (resolve, reject) => {
        try {
            const request = await axios.post(apiUrls.baseURL + apiUrls.forgotPassword, {
                userName: userName
            })
            const response = await request.data
            resolve(response)
        } catch (e) {
            reject(messages.serverError(e.message))
        }
    })
}

export const resetPassword = (resetPassword: ResetPassword) => {
    return new Promise(async (resolve, reject) => {
        try {
            const request = await axios.post(apiUrls.baseURL + apiUrls.resetPassword, resetPassword)
            const response = await request.data
            resolve(response)
        } catch (e) {
            reject(messages.serverError(e.message))
        }
    })
}