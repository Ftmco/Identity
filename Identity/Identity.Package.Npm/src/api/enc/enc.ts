export const encrypt = ({ text, key, revert = false }: { text: string; key: string; revert?: boolean; }) => {
    if (text === null)
        return '';
    let newText = '';
    for (let i = 0; i < text.length; i++) {
        const code = text.charCodeAt(i) + (revert ? key.charCodeAt(Math.abs(key.length - i) % key.length) : key.charCodeAt(i % key.length))
        newText += String.fromCharCode(code);
    }
    return newText;
}

export const decrypt = ({ text, key, revert = false }: { text: string; key: string; revert?: boolean; }) => {
    if (text == null)
        return '';
    let newText = '';
    for (let i = 0; i < text.length; i++) {
        newText += String.fromCharCode(text.charCodeAt(i) - (revert ? key.charCodeAt(Math.abs(key.length - i) % key.length) : key.charCodeAt(i % key.length)));
    }
    return newText;
}