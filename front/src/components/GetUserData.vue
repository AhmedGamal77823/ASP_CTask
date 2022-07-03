<template>
  <div class="row">
    <div class="col-md-12">
        <div v-if="message && message.length" class="alert alert-info">
          {{ message }}
        </div>
    </div>
    <div class="col-md-12">
      <form v-if="!userObject.firstName" @submit.prevent="createUser" class="row g-3">
        <div class="col-md-6">
          <label for="userName" class="form-label">UserName</label>
          <input
            v-model="form.userName"
            type="text"
            class="form-control"
            id="userName"
          />
        </div>
        <div class="col-md-6">
          <label for="Password" class="form-label">Password</label>
          <input
            v-model="form.password"
            type="password"
            class="form-control"
            id="password"
          />
        </div>
        <div class="col-12">
          <button type="submit" class="btn btn-primary">Get User Data</button>
        </div>
      </form>
      <div v-else class="div">
        <p class="mb-0">User Name: {{userObject.userName}}</p>
        <p class="mb-0">First Name: {{userObject.firstName}}</p>
        <p class="mb-0">Father Name: {{userObject.fatherName}}</p>
        <p class="mb-0">Family Name: {{userObject.familyName}}</p>
        <p class="mb-0">Occupation: {{userObject.occupation}}</p>
        <p class="mb-0">Address: {{userObject.address}}</p>
        <p class="mb-0">BirthDate: {{userObject.birthDate}}</p>

        <button class="btn btn-sm btn-danger w-100" @click="userObject = {};message=''">Logout</button>
      </div>
    </div>
  </div>
</template>

<script>
import { defineComponent } from "vue";
import axios from "axios";

export default defineComponent({
  data() {
    return {
      userObject: {
        firstName: null,
        familyName: null,
        fatherName: null,
        userName: null,
        password: null,
        birthDate: null,
        address: null,
        occupation: null,
      },
      form: {
        userName: null,
        password: null
      },
      status: null,
      message: "",
    };
  },
  methods: {
    createUser: function () {
      axios
        .post("api/Users/getData", this.form)
        .then((res) => {
            this.message = res.data.message
            this.userObject = res.data.data;
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
