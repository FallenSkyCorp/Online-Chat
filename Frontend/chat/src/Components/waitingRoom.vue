<template>
    <div></div>
        <div class="container">
        <div class="text">
           Онлайн чат
        </div>
        <form id="form">
           <div class="form-row">
              <div class="input-data">
                 <input 
                 v-model.trim="Username"
                 id="userName" 
                 type="text" 
                 required
                 >
                 <div class="underline"></div>
                 <div class="error">{{ usernameAlreadyExistString }}</div>
                 <label for="">Имя пользователя</label>
              </div>
           </div>
           <div class="form-row">
              <div class="input-data">
                 <input
                 v-model.trim="ChatRoom"
                 id="chatName"
                 type="text"
                 required
                 >
                 <div class="underline"></div>
                 <label for="">Название чата</label>
              </div>
           </div>
           <div class="form-row submit-btn">
            <div class="centre">
               <div class="input-data">
                  <div class="inner"></div>
                  <input 
                  @click.prevent="onSubmit"
                  readonly
                  value="ПРИСОЕДИНИТЬСЯ">
               </div>
            </div>
           </div>
        </form>
    </div>
    <div></div>
</template>

<script>
export default {
    props: {
        joinChat: {
            required: true
        },
        usernameAlreadyExistString: {
            type: String,
            required: true,
        },
    },

    data() {
        return {
            Username: '',
            ChatRoom: ''
        }
    },
    methods: {
        validateInputs() {
            if(this.Username.length < 3 || this.ChatRoom.length < 3) {
                console.log('Слишком короткиое имя пользователя или чата')

                return false;
            }
            return true;
        },

        onSubmit() {
            if(!this.validateInputs()) {
                return;
            }

            this.joinChat(this.Username, this.ChatRoom);
        }
    },
}
</script>

<style scoped>
.container{
    border-radius: 20px;
    background: #fff;
    width: 800px;
    padding: 25px 40px 10px 40px;
    box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
}
.container .text{
    text-align: center;
    font-size: 45px;
    font-weight: 600;
    background: -webkit-linear-gradient(right, #56d8e4,
     #9f01ea, #56d8e4, #9f01ea);
    background-clip: text;
    -webkit-text-fill-color: transparent;
}
.container form{
    padding: 10px 0 0 0;
}
.container form .form-row{
    display: block;
    margin: 35px 0;
}
form .form-row .input-data{
    width: 100%;
    height: 40px;
    margin: 35px 0 0 0;
    position: relative;
}
.input-data input{
    display: block;
    outline: none;
    width: 100%;
    height: 100%;
    border: none;
    font-size: 17px;
    border-bottom: 2px solid rgba(0, 0, 0, 0.12);
}
.input-data input:focus ~ label, 
.input-data input:valid ~ label{
    transform: translateY(-20px);
    font-size: 14px;
    color: #3498db;
}
.input-data label{
    position: absolute;
    pointer-events: none;
    bottom: 10px;
    font-size: 16px;
    transition: all 0.3s ease;
}
.input-data .underline:before{
    position: absolute;
    content: "";
    height: 2px;
    width: 100%;
    background: #3498db;
    transform: scaleX(0);
    transform-origin: center;
    transition: transform 0.3s ease;
}
.input-data input:focus ~ .underline:before,
.input-data input:valid ~ .underline:before{
    transform: scale(1);
}
.submit-btn .input-data{
    overflow: hidden;
    border-radius: 10px;
    height: 45px!important;
    width: 40%!important;
    min-width: 270px;
}
.submit-btn .input-data .inner{
    height: 100%;
    width: 300%;
    position: absolute;
    left:-100%;
    background: -webkit-linear-gradient(right, #56d8e4,
     #9f01ea, #56d8e4, #9f01ea);
     transition: all 0.4s ease;
}
.submit-btn .input-data:hover .inner{
    left: 0;
}
.submit-btn .input-data input{
    background: none;
    border: none;
    color: #fff;
    font-size: 17px;
    font-weight: 500;
    text-transform: uppercase;
    letter-spacing: 1px;
    cursor: pointer;
    text-align: center;
    position: relative;
    z-index: 2;
}
.centre{
    margin: 0px 30%;
}
.valid{
    font-size: 15px;
    font-weight: 500;
}
#registration{
    text-decoration: none;
    text-align: center;
    line-height: 45px;
    padding: 10%;
}
.registr-succes{
    font-weight: 500;
    color: greenyellow;
}
.registr-failed{
    font-weight: 500;
    color: red;
}

@media (max-width:500px){
    .centre{
        margin: 0px 15%;
    }
}
</style>