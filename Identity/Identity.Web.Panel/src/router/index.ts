import Vue from 'vue'
import VueRouter, { NavigationGuardNext, Route } from 'vue-router';
import { RouteConfig } from 'vue-router'

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
            }
        ]
    }
];

const router = new VueRouter({
    routes: routes,
    mode: 'history',
})

//router.beforeEach((to: any, from: Route, next: NavigationGuardNext<Vue>) => {

//})

export default router;