<template>
    <v-col>
        <v-form ref="appForm">
            <v-card>
                <v-col>
                    <v-text-field label="Name"
                                  v-model="application.name"
                                  :rules="[rules.require]"
                                  outlined
                                  clearable
                                  placeholder="Name"></v-text-field>

                    <v-text-field v-model="application.password"
                                  :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
                                  :type="show ? 'text' : 'password'"
                                  :rules="[rules.require,rules.password]"
                                  outlined
                                  clearable
                                  label="Password"
                                  hint="At least 6 characters"
                                  counter
                                  @click:append="show = !show"></v-text-field>

                    <v-col align="center">
                        <v-img v-if="img != ''"
                               :src="img"
                               :lazy-src="img"
                               width="250" />
                    </v-col>

                    <v-file-input @change="profileSelect"
                                  placeholder="Application Profile"
                                  label="Application Profile"
                                  prepend-icon="mdi-paperclip">
                        <template v-slot:selection="{ text }">
                            <v-chip small
                                    label
                                    color="primary">
                                {{ text }}
                            </v-chip>
                        </template>
                    </v-file-input>
                    <v-card-actions>
                        <v-btn block
                               :color="btnColor"
                               @click="submit">
                            <span>{{btnText}}</span>
                        </v-btn>
                    </v-card-actions>
                </v-col>
            </v-card>
        </v-form>
    </v-col>
</template>

<script lang="ts">
    import Vue from 'vue'
    import { apiCall } from '@/api'
    import ApplicationService from '@/api/service/application.service'
    import { messages, rules } from '@/constants'
    import { File } from '../../api/models/application.model'

    export default Vue.extend({
        data: () => ({
            applicationService: new ApplicationService(apiCall),
            application: {
                name: "",
                password: "",
                file: {
                    base64: "",
                    extension: ""
                }
            },
            img: "",
            rules: rules,
            show: false,
            btnText: "Create",
            btnColor: "primary"
        }),
        props: {
            editApp: Object
        },
        mounted() {
            this.checkUpdate()
        },
        methods: {
            checkUpdate() {
                if (this.editApp) {
                    this.application.name = this.editApp.name;
                    this.img = this.editApp.image
                }
            },
            profileSelect(file: any) {
                if (file != null) {
                    let fileReader = new FileReader()
                    fileReader.readAsDataURL(file)
                    fileReader.onload = () => {
                        this.img = fileReader.result
                    }
                } else
                    this.img = ""
            },
            submit() {
                let isValid = this.$refs.appForm.validate()
                if (isValid) {
                    this.$root.$refs.loading.open()
                    this.application.file = this.createFile()
                    this.applicationService.Create(this.application)
                        .then((res) => {
                            if (res.status)
                                this.$emit("submit", {
                                    app: res.result.application
                                })
                            this.showMessage(res.title)
                        }).catch((e) => {
                            this.showMessage(messages.netWorkError(e.message).title)
                        })
                }
                else
                    this.showMessage(messages.invalidForm)
            },
            createFile() {
                let base64items = this.img.split(";")
                let extension = "." + base64items[0].split("/").pop();
                let base64 = base64items[1].split(",").pop();
                return {
                    base64: base64,
                    extension: extension
                }
            },
            showMessage(text: string) {
                this.$root.$refs.snackbar.open(text)
                this.$root.$refs.loading.close()
            }
        }
    })
</script>

<style scoped>
</style>