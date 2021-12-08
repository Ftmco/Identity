<template>
    <v-app>
        <AppBar />

        <v-main>

        </v-main>
    </v-app>
</template>

<script lang="ts">
    import Vue from 'vue';
    import AppBar from '@/components/core/AppBar.vue'

    export default Vue.extend({
        name: 'App',

        components: {
            AppBar
        },

        data: () => ({
            //
        }),
    });
</script>
