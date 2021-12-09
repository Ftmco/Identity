<template>
    <div>

    </div>
</template>

<script lang="ts">
    import Vue from 'vue'

    export default Vue.extend({
        data: () => ({
            applications: []
        }),
        mounted() {
            this.getApplications();
        },
        methods: {
            getApplications() {

            }
        }
    })
</script>

<style scoped>
</style>