<template>
  <AccountBase title="Signup" subTitle="Create Account In Identity" formHeight="100%">
    <template v-slot:image>
      <v-img
        contain
        src="@/assets/images/register.png"
        lazy-src="@/assets/images/register.png"
        width="100%"
        height="100%"
      />
    </template>

    <template v-slot:form>
      <v-form ref="signUpForm">
        <v-text-field
          v-model="signUpModel.userName"
          :rules="[rules.require]"
          outlined
          clearable
          color="blue darken-2"
          label="User Name"
          required
        ></v-text-field>
        <v-text-field
          v-model="signUpModel.fullName"
          :rules="[rules.require]"
          outlined
          clearable
          color="blue darken-2"
          label="Full Name"
          required
        ></v-text-field>
         <v-text-field
          v-model="signUpModel.mobileNo"
          :rules="[rules.require]"
          outlined
          clearable
          color="blue darken-2"
          label="Mobile Number"
          required
        ></v-text-field>
         <v-text-field
          v-model="signUpModel.email"
          :rules="[rules.require]"
          outlined
          clearable
          color="blue darken-2"
          label="E-mail"
          required
        ></v-text-field>
          <v-text-field
          v-model="signUpModel.password"
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
       <v-btn color="primary" block>
           Register
       </v-btn>
        <br />
        <v-row>
          <v-col>
            <v-btn color="info" block :to="{ name: 'ForgotPassword' }">
              <span>Forgot Password</span>
            </v-btn>
          </v-col>
          <v-col>
            <v-btn color="info" block :to="{ name: 'Login' }">
              <span>Login</span>
            </v-btn>
          </v-col>
          <v-col>
           <v-btn color="info" block :to="{name:'ActiveAccount'}">
              <span>Active Account</span>
            </v-btn>
          </v-col>
        </v-row>
    </template>
  </AccountBase>
</template>

<script lang="ts">
import Vue from "vue";
import AccountBase from "@/components/account/AccountBase.vue";
import { rules } from "@/constants";
export default Vue.extend({
  data: () => ({
    signUpModel: {
      userName: "",
      fullName: "",
      mobileNo: "",
      email: "",
      password: "",
    },
    rules:rules,
    show:false
  }),
  components: {
    AccountBase,
  },
});
</script>