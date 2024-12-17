<template>
    <li 
    v-for="message in messageList"
    :class="{ self: message.self, message__wrapper: message.self === undefined }"
    >
      <div class="message">
        <div class="author">{{ message.username }}</div>
         {{ message.body }}
        <div class="time">
          {{ message.time }}
        </div>
      </div>
  </li>   
</template>

<script>
export default {
    props: {
        messageList: {
            type: Array,
                
            required: true
        }
    },
    
}   
</script>

<style scoped>
.message__wrapper{
    display: flex;
    flex-direction: row;
}
.self{
    display: flex;
    flex-direction: row-reverse;
}
.message__wrapper .message{
    word-wrap: break-word;
    display: inline-block;
    background: var(--message-background-color);
    border-radius: 10px;
    min-width: 5%;
    max-width: 60%;
    margin: 10px 0px;
    padding: 10px;
    font-size: 16px;
    color: var(--message-color);
}
.self .message{
    word-wrap: break-word;
    display: inline-block;
    background: var(--self-message-background-color);
    border-radius: 10px;
    min-width: 5%;
    max-width: 60%;
    margin: 10px 0px;
    padding: 10px;
    font-size: 16px;
    color: var(--message-color);
}
.message .author{
    font-size: 10px;
    margin-top: -5px;
    white-space: nowrap; /* Текст не переносится */
    overflow: hidden; /* Обрезаем всё за пределами блока */
    text-overflow: ellipsis; /* Добавляем многоточие */
}
.message .time{
    color: var(--message-color);
    font-weight: 500;
    margin-left: 10px;
    display: inline;
    font-size: 12px;
}
</style>