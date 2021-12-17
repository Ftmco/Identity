<template>
  <div>
    <div>
      <v-form ref="usersForm">
        <v-text-field
          v-if="!showTable"
          v-model="password"
          :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
          append-outer-icon="mdi-magnify"
          :type="show ? 'text' : 'password'"
          :rules="[rules.require, rules.password]"
          outlined
          clearable
          label="Password"
          hint="At least 6 characters"
          counter
          @click:append="show = !show"
          @click:append-outer="loadUsers"
        ></v-text-field>
      </v-form>
    </div>
    <v-data-table
      v-if="showTable"
      :headers="headers"
      :items="users"
      item-key="name"
      class="elevation-1"
      :search="search"
      hide-default-footer
    >
      <template v-slot:top>
        <v-text-field
          v-model="search"
          label="Search Users"
          class="mx-4"
        ></v-text-field>
      </template>

      <template v-slot:item.actions="{ item }">
        <div>
          <v-row>
            <v-col>
              <v-btn outlined block color="red" @click="removeUser(item)">
                <span>Delete</span>
                <v-icon>mdi-trash-can-outline</v-icon>
              </v-btn>
            </v-col>
          </v-row>
        </div>
      </template>
    </v-data-table>
  </div>
</template>


<script lang="ts">
import { apiCall } from "@/api";
import ApplicationService from "@/api/service/application.service";
import { app, messages, rules, userTableHeaders } from "@/constants";
import Vue from "vue";
export default Vue.extend({
  props: {
    appId: String,
    apiKey: String,
  },
  data: () => ({
    users: [],
    appService: new ApplicationService(apiCall),
    headers: userTableHeaders,
    showTable: false,
    search: "",
    show: false,
    password: "",
    rules: rules,
  }),
  methods: {
    loadUsers() {
      let isValid = this.$refs.usersForm.validate();
      if (isValid) {
        this.getUsers();
        this.showTable = true;
      } else this.showMessage(messages.invalidForm);
    },
    getUsers() {
      this.$root.$refs.loading.open();
      this.appService
        .GetUsers({
          apikey: this.apiKey,
          password: this.password,
        })
        .then((res) => {
          if (res.status) this.users = res.result.users;
          else this.showTable = false;
          this.showMessage(res.title);
        })
        .catch((e) => {
          this.showMessage(messages.netWorkError(e.message).title);
        });
    },
    showMessage(text: string) {
      this.$root.$refs.loading.close();
      this.$root.$refs.snackbar.open(text);
    },
  },
});
</script>