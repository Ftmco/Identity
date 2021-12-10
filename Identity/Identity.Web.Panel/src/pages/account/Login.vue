<template>
    <div class="login-container">
        <v-col cols="12" align="center">
            <v-row>
                <v-col cols="12" md="7" sm="6">
                    <v-card>
                        <v-img contain src="@/assets/images/login.png" width="100%"
                               height="500px" />
                    </v-card>
                </v-col>
                <v-col cols="12" md="5" sm="6">
                    <v-card width="100%"
                            height="500px">
                        <v-col>
                            <v-card-title align="center">Login</v-card-title>
                            <v-card-subtitle align="center">Welcome to Identity Panel</v-card-subtitle>
                            <v-divider></v-divider>
                            <v-form ref="loginForm">
                                <v-text-field v-model="login.userName"
                                              :rules="[rules.require]"
                                              outlined
                                              clearable
                                              color="blue darken-2"
                                              label="UserName"
                                              required></v-text-field>

                                <v-text-field v-model="login.password"
                                              :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
                                              :type="show ? 'text' : 'password'"
                                              :rules="[rules.require,rules.password]"
                                              outlined
                                              clearable
                                              label="Password"
                                              hint="At least 6 characters"
                                              counter
                                              @click:append="show = !show"></v-text-field>
                            </v-form>
                            <v-btn color="primary" block @click="loginSubmit">
                                <span>Login</span>
                            </v-btn>
                            <br />
                            <v-row>
                                <v-col>
                                    <v-btn color="info" block>
                                        <span>Forgot Password</span>
                                    </v-btn>
                                </v-col>
                                <v-col>
                                    <v-btn color="info" block>
                                        <span>Sign Up</span>
                                    </v-btn>
                                </v-col>
                                <v-col>
                                    <v-btn color="info" block>
                                        <span>Active Account</span>
                                    </v-btn>
                                </v-col>
                            </v-row>
                        </v-col>
                    </v-card>
                </v-col>
            </v-row>
        </v-col>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue'
    import { messages, rules } from '@/constants'
    import AccountServiec from '../../api/service/account.service'
    import { apiCall } from '../../api'

    export default Vue.extend({
        data: () => ({
            login: {
                userName: "",
                password: ""
            },
            show: false,
            rules: rules,
            accountService: new AccountServiec(apiCall)
        }),
        methods: {
            loginSubmit() {
                let isValid = this.$refs.loginForm.validate()
                if (isValid) {
                    this.accountService.Login(this.login)
                        .then((res) => {
                            if (res.status)
                                window.location = "profile"
                            this.showMessage(res.title)
                        })
                        .catch((e) => {
                            this.showMessage(messages.netWorkError(e.message).title)
                        })
                } else
                    this.showMessage(messages.invalidForm)
            },
            showMessage(message: string) {
                this.$root.$refs.snackbar.open(message)
            }
        }
    })
</script>

<style scoped>
    @import "../../assets/style/account.css";
</style>