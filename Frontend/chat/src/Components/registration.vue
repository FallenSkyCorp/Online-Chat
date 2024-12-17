<template>
    <div class="container">
        <div class="text">
           Регистрация
        </div>
        <form id="form">
           <div class="form-row">
              <div class="input-data">
                 <input
                 v-model="email"
                 id="email" 
                 name="email" 
                 type="email" 
                 required
                 >
                 <div class="underline"></div>
                 <label for="">Эл. Почта</label>
              </div>
           </div>
           <div class="form-row">
              <div class="input-data">
                 <input 
                 v-model="password"
                 id="password"
                 name="password" 
                 type="password" 
                 minlength="3" 
                 pattern="\w*[@\$~%&\?_]+\w*" 
                 required
                 >
                 <div class="underline"></div>
                 <label for="">Пароль</label>
              </div>
           </div>
           <div class="form-row">
            <div class="input-data">
               <input 
               v-model="repeatedPassword"
               id="passConfirm" 
               type="password" 
               minlength="3" 
               required
               >
               <div class="underline"></div>
               <div class="error">{{ this.error }}</div>
               <label for="">Повторите пароль</label>
            </div>
         </div>
           <div class="form-row submit-btn">
            <div class="centre">
               <div class="input-data">
                  <div class="inner"></div>
                  <input @click.prevent="registrateUser" readonly value="ЗАРЕГИСТРИРОВАТЬСЯ">
               </div>
            </div>
           </div>
        </form>
    </div>
</template>

<script>

export default {

    data() {
        return {
            email: '',
            password: '',
            repeatedPassword: '',
            error: '',
        }
    },

    computed: {
        isEqualPassword() {
            return this.password === this.repeatedPassword
        },

        validateFormData() {
            if(!this.email.includes("@")) {
                return 'Невалидная почта'
            }
            if(this.password.length < 3) {
                return 'Слишком короткий пароль'
            }
            if(!this.isEqualPassword) {
                return 'Пароли не совпадают'
            }
            return true;
        },
    },

    methods:{
        async registrateUser() {

            if(this.validateFormData !== true) {
                this.error = this.validateFormData

                return
            }

            this.error = ''
            
            const userRequest = { Email: this.email, Password: this.password }

            const res = await fetch("http://localhost:5099/registration", {
                method: "POST",
                headers: { 
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify(userRequest)
            });

            const userData = await res.json()

            if(res.ok) {
                this.$emit('userLogin', userData)
            }
            else {
                this.error = userData.message;
            }
        },    
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
    text-align: center;
    letter-spacing: 1px;
    cursor: pointer;
    position: relative;
    z-index: 2;
}
.centre{
    margin: 0px 30%;
}

@media (max-width:500px){
    .centre{
        margin: 0px 15%;
    }
}
</style>
