<template>
  <div class="row">
    <div class="col-md-12">
        <div v-if="message && message.length" class="alert alert-info">
          {{ message }}
        </div>
    </div>
    <div class="col-md-12">
            <form @submit.prevent="resetPassword" class="row g-3">
  <div class="col-md-6">
    <label for="userName" class="form-label">UserName</label>
    <input v-model="form.userName" type="text" class="form-control" id="userName">
  </div>
  <div class="col-md-6">
    <label for="oldPassword" class="form-label">Old Password</label>
    <input v-model="form.oldPassword" type="password" class="form-control" id="oldPasswrod">
  </div>
  <div class="col-md-6">
    <label for="newPassword" class="form-label">New Password</label>
    <input v-model="form.newPassword" type="password" class="form-control" id="newPassword">
  </div>
  <div class="col-12">
    <button type="submit" class="btn btn-primary">Reset Password</button>
  </div>
</form>
    </div>
  </div>

</template>

<script> 
import { defineComponent } from "vue";
import axios from "axios";

export default defineComponent({
  data() {
    return {
      form: {
        userName: null,
        oldPassword: null,
        newPassword: null
      },
      status: null,
      message: ""
    };
  },
  methods: {
    resetPassword: function () {
        axios
        .post("api/Users/password/reset", this.form)
        .then((res) => {
            this.message = res.data.message
            this.form = {
                userName: null,
                password: null
            }
        })
        .catch((err) => {
            this.message = err.response.data.message ?? err.response.data.title ?? "Error" 
          console.log(err,err.response.data.message);
        });
    },
  },
});
</script>