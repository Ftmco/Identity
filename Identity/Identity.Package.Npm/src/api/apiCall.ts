import Axios, { AxiosRequestConfig, AxiosRequestHeaders } from "axios";


const _headers: AxiosRequestHeaders = {
    ["I-Authentication"]: ""
}

const _config: AxiosRequestConfig = {
    baseURL: '',
    timeout: 60 * 1000,
    headers: _headers
}

export const addHeader = (key: string, value: string) => {
    _headers[key] = value
}

export const axios = Axios.create(_config);