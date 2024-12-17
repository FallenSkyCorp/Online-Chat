<template>
  <div v-show="!connection" class="containerbody">
    <template v-if="!logined && !connection">
      <nav class="refs">
        <button 
         @click="showDialogRegistration" 
         class="mybtn"
        >
        ЗАРЕГИСТРИРОВАТЬСЯ
        </button>
        |
        <button  
         class="mybtn" 
         @click="showDialogLogin"
        >
        ВОЙТИ
        </button>
      </nav>
      
      <dialogWindow
       :isLoginDialog="isLoginDialog"
       v-model:show="dialogVisible"
      >
        <template #login>
          <login
           @userLogin="userLogin" 
          />
        </template>
        <template #registration>
          <registration 
           @userLogin="userLogin" 
          />
        </template>
      </dialogWindow> 

      <h1 class="title">
        Хз че написать сюда пока что
      </h1>
      <div></div>
    </template>

    <waitingRoom
     v-if="logined
     && !connection" 
     :joinChat="joinChat"
     :usernameAlreadyExistString="usernameAlreadyExistString"
    >
  </waitingRoom>
  </div>

  <chat 
   v-if="logined && connection" 
   :messages="messages"
   :chatName="ChatRoom"
   :closeChat="closeChat"
   @createMessage="createMessage"
  />

</template>

<script>

import dialogWindow from '@/Components/dialogWindow.vue'
import chat from '@/Components/chat.vue'
import waitingRoom from '@/Components/waitingRoom.vue'
import login from '@/Components/login.vue'
import registration from '@/Components/registration.vue'
import { HubConnectionBuilder } from '@microsoft/signalr'

export default {
  components: {
    chat, waitingRoom, login, registration, dialogWindow
  },

  data() {
    return {
      dialogVisible: false,

      isLoginDialog: true,

      logined: false,

      currentUser: {},

      messages: [],

      ChatRoom : '',

      username: '',

      usernameAlreadyExistString: '',

      connection : null,

      joinChat: async (Username, ChatRoom) => {
        var connection = new HubConnectionBuilder()
            .withUrl('http://localhost:5099/chat')
            .withAutomaticReconnect()
            .build()

        connection.on("ReceiveMessage", ( userName, messageString) => {

            const message = {
              id: Date.now(),
              username: userName,
              body: messageString,
              time: this.getCurrentTime(),
            };

            this.messages.push(message)
        });

        try {
            await connection.start();

            const usernameNotValid = await connection.invoke("IsContainsUsername", Username);

            console.log("usernameNotValid: "+ usernameNotValid)

            if(usernameNotValid) {
              this.usernameAlreadyExistString = 'Пользователь с таким именем уже есть в чате.';

              await connection.stop();

              this.connection = null

              return;
            }

            await connection.invoke("JoinChat", { Username, ChatRoom });

            this.connection = connection;

            this.ChatRoom = ChatRoom;

            this.username = Username;

        } catch(error) {
            console.log(error);
        }
      },

      closeChat: async () => {
        await this.connection.stop();

        this.connection = null;
      },
    }
  },

  methods: {
    createMessage(messageString) {
      this.connection.invoke('SendMessage', messageString);

      const message = {
        id: Date.now(),
        username: this.username,
        body: messageString,
        time: this.getCurrentTime(),
        self: true,
      };

      this.messages.push(message)
    },

    showDialogRegistration() {
      this.dialogVisible = true;

      this.isLoginDialog = false;
    },

    showDialogLogin() {
      this.dialogVisible = true;

      this.isLoginDialog = true;
    },

    userLogin(userData) {
      this.currentUser = userData;

      this.logined = true;
    },

    getCurrentTime() {
      const currentDate = new Date();

      const curTime = currentDate.toLocaleTimeString()

      const time = curTime.substring(0, 5)

      return time;
    }
  },
}
</script>

<style>
:root{
    --body-background: #eee2dc;
    --container-border: #edc7b7 3px solid;
    --chat-color: #ac3b61;
    --message-background-color: #edc7b7;
    --message-color: #123c69;
}
:root[theme='dark']{
    --body-background: #0b0c10;
    --container-border: #1f2833 3px solid;
    --chat-color: #66fcf1;
    --message-background-color: #1f2833;
    --self-message-background-color: #114385;
    --message-color: #c5c6c7;
}
*{
  margin: 0;
  padding: 0;
  outline: none;
  box-sizing: border-box;
  font-family: Verdana, Verdana, Geneva, Tahoma, sans-serif;
}
.containerbody{
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-between;
    min-height: 100vh;
    padding: 10px;
    background: linear-gradient(110deg, #56d8e4 10%, #9f01ea 90%);
}
.containerbody .refs{
  align-self: flex-end;
  justify-self: flex-start;
}
.mybtn, .refs{
    background: none;
    border: none;
    color: #fff;
    font-size: 17px;
    font-weight: 500;
    text-transform: uppercase;
    text-decoration: none;
    letter-spacing: 1px;
    cursor: pointer;
    position: relative;
    z-index: 2;
}
.title{
  font-size: 40px;
  color: #fff;
  text-shadow: 0 ;
  justify-content: center;
}
.error{
    font-weight: 500;
    color: #ff0000;
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
      --self-message-background-color: #274061;
      --message-color: #c5c6c7;
    }
}
</style>