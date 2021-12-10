export default function ({ store, next }) {
    if (!store.getters.auth.isLogin) {
        return next({
            name: "Login"
        })
    }
    return next();
}