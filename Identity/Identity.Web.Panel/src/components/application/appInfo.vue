<template>
  <v-col cols="12">
    <v-col cols="12" align="center">
      <v-list-item-avatar size="150" color="grey">
        <v-img
          :src="application.image"
          :lazy-src="application.image"
          :alt="application.name"
        />
      </v-list-item-avatar>
    </v-col>

    <v-col cols="12">
      <v-expansion-panels v-model="panelModel" multiple>
        <v-expansion-panel v-for="panel in panels" :key="panel.id">
          <v-expansion-panel-header>{{ panel.title }}</v-expansion-panel-header>
          <v-expansion-panel-content>
            <component :is="panel.component" v-bind="panel.props"></component>
          </v-expansion-panel-content>
        </v-expansion-panel>
      </v-expansion-panels>
    </v-col>
  </v-col>
</template>

<script lang="ts">
import Vue from "vue";
import ApplicationUsers from "./applicationUsers.vue";

export default Vue.extend({
  props: {
    application: Object,
  },
  components: {
    ApplicationUsers,
  },
  data: () => ({
    users: {},
    panelModel: null,
    panels: {},
  }),
  mounted() {
    this.setPanels();
  },
  methods: {
    setPanels() {
      this.panels = [
        {
          id: 0,
          title: "Users",
          component: ApplicationUsers,
          props: {
            appId: this.application.id,
          },
        },
      ];
    },
  },
});
</script>