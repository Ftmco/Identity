<template>
    <div>
        <AppBar />
        <v-main>
            <router-view />
        </v-main>
        <TheBottomNavigation />
        <TheSnackbar />
        <TheLoading />
    </div>
</template>

<script lang="ts">
    import Vue from 'vue'
    import AppBar from '@/components/core/AppBar.vue'
    import TheBottomNavigation from '@/components/core/TheBottomNavigation.vue'
    import TheSnackbar from "@/components/core/TheSnackbar.vue"
    import TheLoading from '@/components/core/TheLoading.vue'

    export default Vue.extend({

        components: {
            AppBar,
            TheBottomNavigation,
            TheSnackbar,
            TheLoading
        },
    })
</script>

<style scoped>
</style>