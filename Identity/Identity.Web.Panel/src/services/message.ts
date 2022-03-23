import Vue from "vue";

export const showMessage = (vue: Vue, message: string) => {
    if (message.trim().length > 0) {
        (vue.$root.$refs.snackbar as any).open(message);
    }
    closeLoading(vue)
}

export const loading = (vue: Vue) => {
       
    (vue.$root.$refs.loading as any).open();
}

export const closeLoading = (vue: Vue) => {        
    (vue.$root.$refs.loading as any).close();
}

export const openDialog = (vue: Vue) => {
    (vue.$root.$refs.dialog as any).open()
}

export const closeDialog = (vue: Vue) => {
    (vue.$root.$refs.dialog as any).close()
}

export const openLightBox = (vue: Vue) => {
    (vue.$root.$refs.lightBox as any).open()
}

export const closeLightBox = (vue: Vue) => {
    (vue.$root.$refs.lightBox as any).close()
}

export const openConfirm = (vue: Vue, data: any) => {
    (vue.$root.$refs.confirmDialog as any).open(data)
}