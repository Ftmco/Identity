<template>
  <AccountBase title="Forgot Password" subTitle="Forgot Password Identity" formHeight="90vh">
    <template v-slot:image>
      <v-img
        contain
        src="@/assets/images/forgot-password.png"
        lazy-src="@/assets/images/forgot-password.png"
        width="100%"
        height="90vh"
      />
    </template>

    <template v-slot:form>
      <v-form ref="forgotForm">
        <v-text-field
          v-model="userName"
          :rules="[rules.require]"
          outlined
          clearable
          color="blue darken-2"
          label="UserName"
          required
        ></v-text-field>
        <div v-if="inReset">
          <v-text-field
            v-model="resetPassword.code"
            :rules="[rules.require]"
            outlined
            clearable
            color="blue darken-2"
            label="Code"
            required
          ></v-text-field>
          <v-text-field
            v-model="resetPassword.password"
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
        </div>
      </v-form>
    </template>

    <template v-slot:actions>
      <div>
        <v-btn v-if="!inReset" color="primary" @click="sendCode" block>
          Send Code
        </v-btn>
        <v-btn v-else color="primary" block @click="reset">
          Reset Password
        </v-btn>
        <br />
        <v-row>
          <v-col>
            <v-btn color="info" block :to="{ name: 'Login' }">
              <span>Login</span>
            </v-btn>
          </v-col>
          <v-col>
            <v-btn color="info" block :to="{name:'SignUp'}">
              <span>Sign Up</span>
            </v-btn>
          </v-col>
          <v-col>
            <v-btn color="info" block :to="{name:'ActiveAccount'}">
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
import AccountBase from "@/components/account/AccountBase.vue";
import { messages, rules } from "@/constants";

export default Vue.extend({
  data: () => ({
    rules: rules,
    inReset: false,
    userName: "",
    show: false,
    resetPassword: {
      userName: "",
      code: "",
      password: "",
    },
  }),
  components: {
    AccountBase,
  },
  methods: {
    sendCode() {
      let isValid = this.$refs.forgotForm.validate();
      if (isValid) {
        this.inReset = true;
      } else this.showMessage(messages.invalidForm);
    },
    reset() {
      let isValid = this.$refs.forgotForm.validate();
      if (isValid) {
          this.showMessage("valid")
      } else this.showMessage(messages.invalidForm);
    },
    showMessage(text: string) {
      this.$root.$refs.snackbar.open(text);
    },
  },
});
</script>