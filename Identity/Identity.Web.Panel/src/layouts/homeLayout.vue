<template>
    <div>
        <AppBar />
        <v-main>
            <router-view />
        </v-main>
        <TheBottomNavigation />
        <TheSnackbar/>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue'
    import AppBar from '@/components/core/AppBar.vue'
    import TheBottomNavigation from '@/components/core/TheBottomNavigation.vue'
    import TheSnackbar from "@/components/core/TheSnackbar.vue"

    export default Vue.extend({

        components: {
            AppBar,
            TheBottomNavigation,
            TheSnackbar
        },
    })
</script>

<style scoped>
</style>