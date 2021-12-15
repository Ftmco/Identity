import Axios, { AxiosRequestConfig } from 'axios'
import Account from '@/services/account'

const _confing: AxiosRequestConfig = {
    baseURL: "https://localhost:7130/api/",
    timeout: 60 * 1000,
    headers: {
        "Content-Type": "application/json",
        "I-Authentication": Account.authenticationToken(),
    },

}

export const changeConfigHeader = (key: any, value: string) => {
    _confing.headers[key] = value;
};

export const apiCall = Axios.create(_confing)

