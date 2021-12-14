<template>
  <div class="account-container">
    <v-col cols="12" align="center">
      <v-row>
        <v-col cols="12" md="7" sm="6">
          <v-card>
            <slot name="image"></slot>
          </v-card>
        </v-col>
        <v-col cols="12" md="5" sm="6">
          <v-card width="100%" :height="formHeight">
            <v-col>
              <v-card-title align="center">{{ title }}</v-card-title>
              <v-card-subtitle align="center">{{ subTitle }}</v-card-subtitle>
              <v-divider></v-divider>
              <br/>
              <slot name="form"> </slot>
              <slot name="actions"> </slot>
            </v-col>
          </v-card>
        </v-col>
      </v-row>
    </v-col>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
export default Vue.extend({
  props: {
    title: String,
    subTitle: String,
    formHeight:String
  },
});
</script>
