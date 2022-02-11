import Axios, { AxiosRequestConfig, AxiosRequestHeaders } from "axios";

const _headers: AxiosRequestHeaders = {
    ["Content-Type"]: "application/json",
    ["Connection"]: "Keep-alive",
}

const _config: AxiosRequestConfig = {
    baseURL: 'https://localhost:7130/api/',
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
}

export const axios = Axios.create(_config);