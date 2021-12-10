export default function ({ store, next }) {
    if (store.getters.auth.isLogin) {
        return next({
            name: "Profile"
        })
    }
    return next();
}