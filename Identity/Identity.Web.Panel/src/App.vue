<template>
  <v-app>
    <router-view />
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";
import { changeBaseUrl } from "fteam.identity.package/src/constants";

export default Vue.extend({
  name: "App",

  data: () => ({
    //
  }),
  created() {
    changeBaseUrl("http://localhost:5130");
  },
});
</script>
