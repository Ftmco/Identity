<template>
  <AccountBase
    title="Login"
    subTitle="Welcome To Identity Panel"
    formHeight="500px"
  >
    <template v-slot:image>
      <v-img
        contain
        src="@/assets/images/login.png"
        lazy-src="@/assets/images/login.png"
        width="100%"
        height="500px"
      />
    </template>
    <template v-slot:form>
      <v-form ref="loginForm">
        <v-text-field
          v-model="login.userName"
          :rules="[rules.require]"
          outlined
          clearable
          color="blue darken-2"
          label="UserName"
          required
        ></v-text-field>

        <v-text-field
          v-model="login.password"
          :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
          :type="show ? 'text' : 'password'"
          :rules="[rules.require, rules.password]"
          outlined
          clearable
          label="Password"
          hint="At least 6 characters"
          counter
          @click:append="show = !show"
        ></v-text-field>
      </v-form>
    </template>
    <template v-slot:actions>
      <div>
        <v-btn color="primary" block @click="loginSubmit">
          <span>Login</span>
        </v-btn>
        <br />
        <v-row>
          <v-col>
            <v-btn color="info" block :to="{ name: 'ForgotPassword' }">
              <span>Forgot Password</span>
            </v-btn>
          </v-col>
          <v-col>
            <v-btn color="info" block :to="{ name: 'SignUp' }">
              <span>Sign Up</span>
            </v-btn>
          </v-col>
          <v-col>
            <v-btn color="info" block :to="{ name: 'ActiveAccount' }">
              <span>Active Account</span>
            </v-btn>
          </v-col>
        </v-row>
      </div>
    </template>
  </AccountBase>
</template>

<script lang="ts">
import Vue from "vue";
import { messages, rules } from "@/constants";
import AccountServiec from "@/api/service/account.service";
import { apiCall } from "@/api";
import AccountBase from "@/components/account/AccountBase.vue";

export default Vue.extend({
  components: { AccountBase },
  data: () => ({
    login: {
      userName: "",
      password: "",
    },
    show: false,
    rules: rules,
    accountService: new AccountServiec(apiCall),
  }),
  methods: {
    loginSubmit() {
      let isValid = this.$refs.loginForm.validate();
      if (isValid) {
        this.accountService
          .Login(this.login)
          .then((res) => {
            if (res.status) window.location = "profile";
            this.showMessage(res.title);
          })
          .catch((e) => {
            this.showMessage(messages.netWorkError(e.message).title);
          });
      } else this.showMessage(messages.invalidForm);
    },
    showMessage(message: string) {
      this.$root.$refs.snackbar.open(message);
    },
  },
});
</script>

<style scoped>
@import "../../assets/style/account.css";
</style>