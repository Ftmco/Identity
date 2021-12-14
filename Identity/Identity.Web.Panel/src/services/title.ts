export const changeTitle = (title: string) => {
    let titleElement = document.getElementsByTagName("title")[0]
    titleElement.innerHTML = title
}