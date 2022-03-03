import Crypto from 'crypto-js'

export const keyMaker = (requestUrl: string) => {
    const utf8 = Crypto.enc.Utf8.parse(requestUrl)
    const base64 = Crypto.enc.Base64.stringify(utf8)
    return base64
}