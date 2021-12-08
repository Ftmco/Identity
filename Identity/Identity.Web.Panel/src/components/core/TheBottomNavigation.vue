<template>
    <div>
        <v-bottom-navigation v-model="page"
                             dark
                             shift>
            <v-btn>
                <span>Video</span>

                <v-icon>mdi-television-play</v-icon>
            </v-btn>

            <v-btn>
                <span>Music</span>

                <v-icon>mdi-music-note</v-icon>
            </v-btn>

            <v-btn>
                <span>Book</span>

                <v-icon>mdi-book</v-icon>
            </v-btn>

            <v-btn>
                <span>Image</span>

                <v-icon>mdi-image</v-icon>
            </v-btn>
        </v-bottom-navigation>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue'

    export default Vue.extend({
        data: () => ({
            page: 1
        })
    })
</script>

<style scoped>
</style>