<template>
    <div class="message-input-container">
            <div class="message-input">
                <input
                 class="m-input" 
                 placeholder="Написать сообщение..."
                 @input="messageInput"
                 @keyup.enter="createMessage"
                >
                <button 
                 class="submit__button"
                 type="submit"
                 @click="createMessage"
                >
                Отправить
                </button>
            </div>
          </div>
</template>

<script>
export default {
    data() {
        return {
            messageString: '',
        }
    },

    methods: {
        messageInput(event) {
            this.messageString = event.target.value;
        },

        createMessage(event) {
            event.preventDefault();

            if(!this.messageString.trim().length) {        
                event.target.value = '';

                return;
            }

            this.$emit('createMessage', this.messageString);

            this.messageString = '';

            event.target.value = '';      
        }
    }
}
</script>

<style scoped>
.message-input-container{
    display: flex;
    align-self: flex-end;
    position: relative;
    bottom: 0;
    left: 0;
    flex-direction: row;
    width: 100%;
    height: auto;
    background-color: var(--message-background-color);
    margin-top: auto;
}
.message-input{
    min-height: 100%;
    width: 100%;
    display: flex;
}
.m-input{
    box-sizing: border-box;
    background: transparent;
    color: var(--message-color);
    border: none;
    display: block;
    min-height: 100%;
    width: 100%;
    padding: 10px;
}
.m-input::placeholder{
    color: var(--message-color);
}
.submit__button{
    display: block;
    cursor: pointer;
    border: none;
    margin: auto;
    height: 50px;
    color: var(--message-color);
    background-color: var(--message-background-color);
    padding: 5px;
}
</style>