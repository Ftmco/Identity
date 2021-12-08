<template>
    <div>
        <v-app-bar app
                   color="primary"
                   dark>
            <div class="d-flex align-center">
                <v-app-bar-nav-icon @click="drawer"></v-app-bar-nav-icon>
                <!--<v-img alt="Vuetify Logo"
                       class="shrink mr-2"
                       contain
                       src="@/assets/R.png"
                       transition="scale-transition"
                       width="40" />-->
                <v-lable>Identity Panel</v-lable>
            </div>

            <v-spacer></v-spacer>
        </v-app-bar>
        <TheNavigation />
    </div>
</template>

<script lang="ts">
    import Vue from 'vue'
    import TheNavigation from "./TheNavigation.vue"

    export default Vue.extend({
        components: {
            TheNavigation
        },
        methods: {
            drawer() {
                this.$root.$refs.navigationDrawer.open();
            }
        }
    })
</script>

<style scoped>
</style>