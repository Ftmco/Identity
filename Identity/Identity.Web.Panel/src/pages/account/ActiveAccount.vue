<template>
  <AccountBase
    title="Active Account"
    subTitle="Active Account Identity"
    formHeight="500px"
  >

    <template v-slot:image>
      <v-img
        contain
        src="@/assets/images/reset-password.png"
        lazy-src="@/assets/images/reset-password.png"
        width="100%"
        height="500px"
      />
    </template>

    <template v-slot:form>
      <v-form ref="activeForm">
        <v-text-field
          v-model="activeModel.userName"
          :rules="[rules.require]"
          outlined
          clearable
          color="blue darken-2"
          label="User Name"
          required
        ></v-text-field>
        <v-text-field
          v-model="activeModel.code"
          :rules="[rules.require]"
          outlined
          clearable
          color="blue darken-2"
          label="Active Code"
          required
        ></v-text-field>
      </v-form>
    </template>
    
    <template v-slot:actions>
      <div>
        <v-btn color="primary" block>
          <span>Active Account</span>
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
            <v-btn color="info" block :to="{ name: 'Login' }">
              <span>Login</span>
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
import { rules } from "@/constants";
export default Vue.extend({
  data: () => ({
    activeModel: {
      userName: "",
      code: "",
    },
    rules: rules,
  }),
  mounted() {
    this.activeModel.userName = this.$route.query.userName.toString();
  },
  components: {
    AccountBase,
  },
});
</script>