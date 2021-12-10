import Vue from 'vue'
import VueRouter, { NavigationGuardNext, Route } from 'vue-router';
import { RouteConfig } from 'vue-router'
import guest from './middleware/guest';
import store from "@/store/index"
import pipeline from './pipeline';
import auth from './middleware/auth';

Vue.use(VueRouter)

const routes: RouteConfig[] = [
    {
        path: "/",
        component: () => import("@/layouts/homeLayout.vue"),
        meta: (route: Route) => ({
            route: route,
            title: 'Home'
        }),
        children: [
            {
                path: "",
                name: "Home",
                component: () => import("@/pages/home/index.vue"),
                meta: (route: Route) => ({
                    route: route,
                    title: 'Home'
                })
            },
            {
                path: "/application",
                name: "Application",
                component: () => import("@/pages/application/application.vue"),
                meta: (route: Route) => ({
                    route: route,
                    title: 'Application'
                })
            },
            {
                path: "/account/login",
                name: "Login",
                component: () => import("@/pages/account/Login.vue"),
                meta: (route: Route) => ({
                    route: route,
                    title: 'Login',
                    middleware: guest
                })
            },
            {
                path: "/account/profile",
                name: "Profile",
                component: () => import("@/pages/account/Profile.vue"),
                meta: (route: Route) => ({
                    route: route,
                    title: 'Profile',
                    middleware: auth
                })
            }
        ]
    }
];

const router = new VueRouter({
    routes: routes,
    mode: 'history',
})

router.beforeEach((to: any, from: Route, next: NavigationGuardNext<Vue>) => {

    let meta = to.meta(to)

    console.log(typeof(to.meta))

    if (!meta.middleware) {
        return next()
    }

    const middleware = meta.middleware
    const context = {
        to,
        from,
        next,
        store
    }

    return middleware({
        ...context,
        next: pipeline(context, middleware, 1)
    })

})

export default router;