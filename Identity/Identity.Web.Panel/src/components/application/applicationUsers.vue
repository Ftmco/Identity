<template>
  <div>
    <v-data-table
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
    </v-data-table>
  </div>
</template>


<script lang="ts">
import { apiCall } from "@/api";
import ApplicationService from "@/api/service/application.service";
import { app, messages, userTableHeaders } from "@/constants";
import Vue from "vue";
export default Vue.extend({
  props: {
    appId: String,
  },
  data: () => ({
    users: [],
    appService: new ApplicationService(apiCall),
    headers: userTableHeaders,
    search: "",
  }),
  beforeMount() {
    this.getUsers();
  },
  methods: {
    getUsers() {
      this.$root.$refs.loading.open();
      this.appService
        .GetUsers(app)
        .then((res) => {
          if (res.status) this.users = res.result.users;
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