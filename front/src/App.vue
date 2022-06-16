<template>
    <div class="container app">
        <div class="row">
            <div class="col-md-10 col-12">
                <div class="row">
                    <div class="col-md-7 bg-white">
                        <div class="p-3">
                            <h3>What You want to do ?</h3>
                            <hr>
                            <div class="accordion" id="accordionExample">
                                <div class="accordion-item">
                                  <h2 class="accordion-header" id="headingOne">
                                    <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                      Create User
                                    </button>
                                  </h2>
                                  <div id="collapseOne" class="accordion-collapse collapse" aria-labelledby="headingOne" data-bs-parent="#accordionExample">
                                    <div class="accordion-body">
                                      <create-user />
                                    </div>
                                  </div>
                                </div>
                                <div class="accordion-item">
                                  <h2 class="accordion-header" id="headingTwo">
                                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                        Reset Password
                                    </button>
                                  </h2>
                                  <div id="collapseTwo" class="accordion-collapse collapse" aria-labelledby="headingTwo" data-bs-parent="#accordionExample">
                                    <div class="accordion-body">
                                      <reset-password />
                                    </div>
                                  </div>
                                </div>
                                <div class="accordion-item">
                                  <h2 class="accordion-header" id="headingThree">
                                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                        Get User Data
                                    </button>
                                  </h2>
                                  <div id="collapseThree" class="accordion-collapse collapse" aria-labelledby="headingThree" data-bs-parent="#accordionExample">
                                    <div class="accordion-body">
                                      <get-user-data />
                                    </div>
                                  </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5 text-white dark">
                        <div class="p-3">
                            <h2>Welcome</h2>
                            <p>
                                Here you can create new user, reset your password and get your personal data !!
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
   </div>
</template>

<script>
import CreateUser from './components/CreateUser.vue';
import ResetPassword from "./components/ResetPassword.vue";
import GetUserData from "./components/GetUserData.vue"
export default {
  name: "App",
  components: {
    ResetPassword,
    CreateUser,
    GetUserData
  },
};
</script>

<style>
        .app {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
        }
        body {
            background-color: #ecf0f1;
        }
        .dark {
            background-color: #2c3e50;
        }
</style>
