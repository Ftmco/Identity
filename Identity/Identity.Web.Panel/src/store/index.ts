import Vue from 'vue'
import Vuex from 'vuex'
import Account from '@/services/account'

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        user: {
            isLogin: Account.isAuthenticated()
        }
    },
    getters: {
        auth(state) {
            return state.user
        }
    },
    mutations: {},
    actions: {},
    modules: {}
})