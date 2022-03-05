import Axios, { AxiosRequestConfig, AxiosRequestHeaders } from "axios";
import { apiUrls } from "../constants";

const _headers: AxiosRequestHeaders = {
    ["Content-Type"]: "application/json",
    ["Connection"]: "Keep-alive",
    ["I-Authentication"]: localStorage.getItem("I-Authentication")?.toString() ?? ""
}

const _config: AxiosRequestConfig = {
    baseURL: apiUrls.baseURL,
    timeout: 60 * 1000,
    headers: _headers
}

/**
 * Add header to Axios Request Header
 * @param key Header Key for authentication add I-Authentication
 * @param value Header Value
 */
export const addHeader = (key: string, value: string) => {
    _headers[key] = value
    _config.headers = _headers
}

export const apiCall = Axios.create(_config);