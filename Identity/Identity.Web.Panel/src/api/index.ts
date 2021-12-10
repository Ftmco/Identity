import Axios, { AxiosRequestConfig } from 'axios';

const _confing: AxiosRequestConfig = {
    baseURL: "https://localhost:44343/api/",
    timeout: 60 * 1000,
    headers: {
        "Content-Type": "application/json",
        //Authorization: Account.authenticationToken(),
    },
}

export const changeConfigHeader = (key: any, value: string) => {
    _confing[key] = value;
};

export const apiCall = Axios.create(_confing)