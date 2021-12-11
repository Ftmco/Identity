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
                        <v-btn color="primary" text @click="newApp">
                            <span>New Application</span>
                            <v-icon>mdi-plus</v-icon>
                        </v-btn>
                    </template>
                </v-text-field>
            </template>
            <template v-slot:item.image="{item}">
                <v-img :src="item.image"
                       :lazy-src="item.image"
                       width="100" />
            </template>
            <template v-slot:item.actions="{item}">
                <div>
                    <v-row>
                        <v-col>
                            <v-btn outlined
                                   block
                                   color="red"
                                   @click="deleteApp(item)">
                                <span>Delete</span>
                                <v-icon>mdi-trash-can-outline</v-icon>
                            </v-btn>
                        </v-col>
                        <v-col>
                            <v-btn outlined
                                   block
                                   color="warning"
                                   @click="editApp(item)">
                                <span>Edit</span>
                                <v-icon>mdi-pencil</v-icon>
                            </v-btn>
                        </v-col>
                        <v-col>

                            <v-btn outlined
                                   block
                                   color="info">
                                <span>App Info</span>
                                <v-icon>mdi-information-outline</v-icon>
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
        <ConfirmDialog @action="deleteConfirm"
                       :title="confirmDialog.title"
                       :text="confirmDialog.text"
                       :agreeText="confirmDialog.agreeText"
                       :disagreeText="confirmDialog.disagreeText" />
        <Appdialog :title="dTitle" :titleColor="dColor">
            <template v-slot:body>
                <component @submit="submit" v-bind="props" :is="comp">

                </component>
            </template>
        </Appdialog>
    </v-col>
</template>

<script lang="ts">
    import Vue from 'vue'
    import ApplicationService from '@/api/service/application.service';
    import ConfirmDialog from "@/components/core/ConfirmDialog.vue"
    import { apiCall } from '@/api';
    import { applicationTableHeaders, messages } from '@/constants';
    import Appdialog from "@/components/core/AppDialog.vue"
    import cuApp from "@/components/application/cuApplication.vue"

    export default Vue.extend({
        data: () => ({
            applications: [],
            confirmDialog: {
                title: "",
                text: "",
                agreeText: "",
                disagreeText: "",
            },
            props: {

            },
            comp: cuApp,
            dTitle: "",
            dColor: "primary",
            page: 0,
            loading: false,
            pageCount: 0,
            headers: applicationTableHeaders,
            search: "",
            applicationService: new ApplicationService(apiCall)
        }),
        components: {
            ConfirmDialog,
            Appdialog
        },
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
                    .then(() => {
                        this.showMessages(`Text copied ${key}`)
                    }).catch((e) => {
                        this.showMessages(`Faild to copy ${e.message}`)
                    })
            },
            deleteApp(item: any) {
                this.confirmDialog = {
                    agreeText: "Delete",
                    disagreeText: "Cancel",
                    title: "Delete Application",
                    text: `Are you sure to delete : ${item.name}`
                }
                this.$root.$refs.confirmDialog.open(item.id)
            },
            deleteConfirm(confirm: any) {
                if (confirm.agree) {
                    this.applicationService.Delete(confirm.data)
                        .then((res) => {
                            if (res.status)
                                this.removeItem(res.result.id)

                            this.showMessages(res.title)
                        })
                        .catch((e) => {
                            this.showMessages(messages.netWorkError(e.message).title)
                        })
                }
            },
            removeItem(id: any) {
                let app = this.applications.filter((app: any) => app.id == id);
                if (app.length > 0) {
                    let index = this.applications.indexOf(app[0])
                    if (index > -1) {
                        this.applications.splice(index, 1)
                    }
                }
            },
            newApp() {
                this.dTitle = "Create New Application"
                this.dColor = "primary"
                this.props = {}
                this.$root.$refs.dialog.open()
            },
            editApp(item: any) {
                this.dTitle = `Update ${item.name}`
                this.dColor = "warning"
                this.props.editApp = item
                this.$root.$refs.dialog.open()
            },
            submit(data: any) {
                this.$root.$refs.dialog.close()
                if (data.update) {
                    this.removeItem(data.app.id)
                }
                this.applications.push(data.app)
            },
            showMessages(message: string) {
                this.$root.$refs.snackbar.open(message)
            }
        }
    })
</script>

<style scoped>
</style>