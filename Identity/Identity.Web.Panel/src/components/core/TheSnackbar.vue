<template>
    <v-snackbar :timeout="timeout" v-model="snackbar">
        {{ text }}

        <template v-slot:action="{ attrs }">
            <v-btn color="pink" text v-bind="attrs" @click="snackbar = false">
                <v-icon>mdi-close</v-icon>
            </v-btn>
        </template>
    </v-snackbar>
</template>

<script lang="ts">
    import Vue from 'vue'

    export default Vue.extend({
        created() {
            this.$root.$refs.snackbar = this;
        },
        props: {
            timeout: {
                type: Number,
                default: 3500,
            },
        },
        data: () => ({
            snackbar: false,
            text: "",
        }),
        methods: {
            open(text: string) {
                this.snackbar = true;
                this.text = text;
            },
            close() {
                this.text = "";
                this.snackbar = false;
            },
        },
    })
</script>