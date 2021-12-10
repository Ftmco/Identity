<template>
    <v-col cols="auto">
        <v-dialog transition="dialog-top-transition"
                  v-model="dialog"
                  :fullscreen="fullscreen"
                  max-width="800px"
                  persistent>
            <v-card v-if="dialog">
                <v-toolbar :color="titleColor" dark>
                    {{ title }}
                    <v-spacer></v-spacer>
                    <v-btn icon @click="dialog = false">
                        <v-icon>mdi-close</v-icon>
                    </v-btn>
                </v-toolbar>
                <slot name="body"></slot>
            </v-card>
        </v-dialog>
    </v-col>
</template>

<script>
    import Vue from 'vue'

    export default Vue.extend({
        props: {
            title: String,
            fullscreen: {
                type: Boolean,
                default: window.screen.width <= 790
            },
            titleColor: {
                type: String,
                default: "primary",
            },
        },
        data: () => ({
            dialog: false,
        }),
        created() {
            this.$root.$refs.dialog = this;
        },
        methods: {
            open() {
                this.dialog = true;
            },
            close() {
                this.dialog = false;
            },
        },
    });
</script>