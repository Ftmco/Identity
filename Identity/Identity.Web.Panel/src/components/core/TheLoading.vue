<template>
    <v-dialog v-model="dialog" hide-overlay persistent width="300">
        <v-card color="primary" dark>
            <v-card-text>
                Please Wait
                <v-progress-linear indeterminate
                                   color="white"
                                   class="mb-0"></v-progress-linear>
            </v-card-text>
        </v-card>
    </v-dialog>
</template>

<script>
    import Vue from 'vue'

    export default Vue.extend({
        data: () => ({
            dialog: false,
        }),
        created() {
            this.$root.$refs.loading = this;
        },
        methods: {
            open() {
                this.dialog = true;
            },
            close() {
                this.dialog = false;
            },
        },
    })
</script>