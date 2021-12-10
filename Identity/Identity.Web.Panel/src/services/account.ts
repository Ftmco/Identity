
export default {
    authenticationToken() {
        return localStorage.getItem("I-Authentication");
    },
    isAuthenticated() {
        let token = this.authenticationToken()
        return token != null
    },
};
