<template>
    <div class="dialog" v-if="show" @click.stop="hideDialog">
        <div @click.stop class="dialog__content">
            <slot v-if="isLoginDialog" name="login"></slot>
            <slot v-else-if='!isLoginDialog' name="registration"> </slot>
        </div>
    </div>
</template>

<script>
export default {

    name: 'dialogWindow',

    props: {
        show: {
            type: Boolean,

            default: false
        },

        isLoginDialog: {
            type: Boolean,

            required: true
        }
    },

    methods: {
        hideDialog() {
            this.$emit('update:show', false)
        }
    },
}
</script>

<style scoped>
.dialog{
    top: 0;
    bottom: 0;
    right: 0;
    left: 0;
    background-color: rgba(0, 0, 0, 0.5);
    position: fixed;
    display: flex;
}
.dialog__content{
    margin: auto;
}
</style>