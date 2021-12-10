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
                              class="mx-4"></v-text-field>
            </template>
            <template v-slot:item.image="{item}">
                <v-img :src="item.src"
                       :lazy-src="item.src" />
            </template>
            <template v-slot:item.acions>
                <div>
                    <v-btn outlined
                           color="red">
                        <span>Delete</span>
                        <v-icon>mdi-trash</v-icon>
                    </v-btn>

                    <v-btn outlined
                           color="warning">
                        <span>Edit</span>
                        <v-icon>mdi-trash</v-icon>
                    </v-btn>

                    <v-btn outlined
                           color="primary">
                        <span>Copy Key</span>
                        <v-icon>mdi-trash</v-icon>
                    </v-btn>
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
            showMessages(message: string) {
                this.$root.$refs.snakBar.open(message)
            }
        }
    })
</script>

<style scoped>
</style>