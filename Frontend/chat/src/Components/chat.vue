<template>
  <div class="wrap">
    <div class="container">
      <div class="inner">
        <div class="chat-name">{{ chatName }}</div>
        <ul>
          <messageList :messageList="messages"/> 
        </ul>  
      </div> 
      <messageInput 
       @createMessage="createMessage"
      />     
    </div>
  </div>
</template>

<script>
import messageList from "@/Components/messageList.vue"
import messageInput from '@/Components/messageInput.vue'

export default {
  components: {
    messageList, messageInput
  },
  props: {
     messages: {
        type: Array,
        required: true
     },

     chatName: {
        type: String,
        required: true
     },

     closeChat: {
      required: true
     }
  },

  methods: {
    createMessage(messageString) {
        this.$emit('createMessage', messageString)
    }
  },
}
</script>

<style scoped>
.wrap{
    display: flex;
    box-sizing: border-box;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    min-height: 100vh;
    background: var(--body-background);
}
.container{
    display: flex;
    flex-direction: column;
    border: var(--container-border);
    min-width: 80%;
    height: 90vh;
}
.container .inner{
    padding: 10px 20px 10px 20px;
}
.container .chat-name{
    text-align: center;
    font-size: 40px;
    font-weight: 600;
    color: var(--chat-color);
}
@media (max-width:500px){
  .container .message-wrapper .message{
      font-size: 14px;
  }
  .submitMessage{
      width: 80px;
      font-size: 12px;
  }
  .message .time{
      margin-left: 5px;
      font-size: 10px;
  }
}
@media (prefers-color-scheme: dark){
    :root{
        --body-background: #0b0c10;
        --container-border: #1f2833 3px solid;
        --chat-color: #66fcf1;
        --message-background-color: #1f2833;
        --message-color: #c5c6c7;
    }
}
</style>