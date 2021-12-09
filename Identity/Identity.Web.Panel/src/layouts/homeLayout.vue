<template>
    <div>
        <AppBar />
        <v-main>
            <router-view />
        </v-main>
        <TheBottomNavigation />
    </div>
</template>

<script lang="ts">
    import Vue from 'vue'
    import AppBar from '@/components/core/AppBar.vue'
    import TheBottomNavigation from '@/components/core/TheBottomNavigation.vue'

    export default Vue.extend({

        components: {
            AppBar,
            TheBottomNavigation
        },
    })
</script>

<style scoped>
</style>