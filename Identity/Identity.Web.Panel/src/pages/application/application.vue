<template>
    <v-col cols="12">
        <v-data-table hide-default-footer
                      :headers="headers"
                      :items="applications"
                      item-key="id"
                      class="elevation-1"
                      :search="search"
                      :loading="loading">
            <template v-slot:top>
                <v-text-field v-model="search"
                              label="Search Applications"
                              class="mx-4">
                    <template v-slot:append>
                        <v-btn color="primary" text>
                            <span>New Application</span>
                            <v-icon>mdi-plus</v-icon>
                        </v-btn>
                    </template>
                </v-text-field>
            </template>
            <template v-slot:item.image="{item}">
                <v-img :src="item.src"
                       :lazy-src="item.src" />
            </template>
            <template v-slot:item.actions="{item}">
                <div>
                    <v-row>
                        <v-col>
                            <v-btn outlined
                                   block
                                   color="red">
                                <span>Delete</span>
                                <v-icon>mdi-trash-can-outline</v-icon>
                            </v-btn>
                        </v-col>
                        <v-col>
                            <v-btn outlined
                                   block
                                   color="warning">
                                <span>Edit</span>
                                <v-icon>mdi-pencil</v-icon>
                            </v-btn>
                        </v-col>
                        <v-col>

                            <v-btn outlined
                                   block
                                   color="primary"
                                   @click="copyKey(item.apiKey)">
                                <span>Copy Key</span>
                                <v-icon>mdi-clipboard-multiple-outline</v-icon>
                            </v-btn>
                        </v-col>
                    </v-row>
                </div>
            </template>
            <template v-slot:body.append>
                <div class="text-center">
                    <v-pagination v-model="page"
                                  :length="pageCount"
                                  :total-visible="5"
                                  prev-icon="mdi-menu-left"
                                  next-icon="mdi-menu-right"></v-pagination>
                </div>
            </template>
        </v-data-table>
    </v-col>
</template>

<script lang="ts">
    import Vue from 'vue'
    import ApplicationService from '@/api/service/application.service';
    import { apiCall } from '@/api';
    import { applicationTableHeaders, messages } from '@/constants';

    export default Vue.extend({
        data: () => ({
            applications: [],
            page: 0,
            loading: false,
            pageCount: 0,
            headers: applicationTableHeaders,
            search: "",
            applicationService: new ApplicationService(apiCall)
        }),
        mounted() {
            this.getApplications(this.page);
        },
        methods: {
            getApplications(page: number) {
                this.applicationService.GetApplications(page, 10)
                    .then((res) => {
                        if (res.status) {
                            this.applications = res.result.applications
                            this.pageCount = res.result.pageCount
                        }
                        this.showMessages(res.title)
                    }).catch((e) => {
                        this.showMessages(messages.netWorkError(e.message).title)
                    })
            },
            copyKey(key: string) {
                navigator.clipboard.writeText(key)
                    .then((res) => {
                        this.showMessages(`Text copied ${key}`)
                    }).catch((e) => {
                        this.showMessages(`Faild to copy ${e.message}`)
                    })
            },
            showMessages(message: string) {
                this.$root.$refs.snackbar.open(message)
            }
        }
    })
</script>

<style scoped>
</style>