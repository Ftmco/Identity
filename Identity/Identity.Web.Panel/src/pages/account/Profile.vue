<template>
  <v-col cols="12"> </v-col>
</template>

<script lang="ts">
import Vue from "vue";
import ProfileService from "@/api/service/profile.service";
export default Vue.extend({
  data: () => ({
    profileService: new ProfileService(),
  }),
});
</script>