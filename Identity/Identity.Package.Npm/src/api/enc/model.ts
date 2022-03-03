import { decrypt, encrypt } from "./enc"
import { keyMaker } from "./keyMaker"

export const createEncModel = (model: any, reqUrl: string) => {
    const key = keyMaker(reqUrl)
    const encrypted = encrypt({
        key: key,
        text: JSON.stringify(model),
        revert: false
    })
    return {
        data: encrypted
    }
}

export const decryptResponse = (res: any, reqUrl: string) => {
    const key = keyMaker(reqUrl)
    const decrypted = decrypt({
        key: key,
        text: res.data,
        revert: false
    })
    const model = JSON.parse(decrypted)
    return model
}