<template>
  <form class="row" @submit.prevent="createUser">
    <div class="col-md-12">
        <div v-if="message && message.length" class="alert alert-info">
          {{ message }}
        </div>
    </div>
    <div class="col-md-6 mb-3">
      <label for="userName" class="form-label">userName</label>
      <input
        required
        v-model="form.userName"
        type="text"
        class="form-control"
        id="userName"
      />
    </div>
    <div class="col-md-6 mb-3">
      <label for="firstName" class="form-label">firstName</label>
      <input
        required
        v-model="form.firstName"
        type="text"
        class="form-control"
        id="firstName"
      />
    </div>
    <div class="col-md-6 mb-3">
      <label for="familyName" class="form-label">familyName</label>
      <input
        required
        v-model="form.familyName"
        type="text"
        class="form-control"
        id="familyName"
      />
    </div>
    <div class="col-md-6 mb-3">
      <label for="fatherName" class="form-label">fatherName</label>
      <input
        required
        v-model="form.fatherName"
        type="text"
        class="form-control"
        id="fatherName"
      />
    </div>
    <div class="col-md-6 mb-3">
      <label for="password" class="form-label">password</label>
      <input
        required
        v-model="form.password"
        type="password"
        class="form-control"
        id="password"
      />
    </div>
    <div class="col-md-6 mb-3">
      <label for="occupation" class="form-label">occupation</label>
      <input
        required
        v-model="form.occupation"
        type="text"
        class="form-control"
        id="occupation"
      />
    </div>
    <div class="col-md-6 mb-3">
      <label for="address" class="form-label">address</label>
      <input
        required
        v-model="form.address"
        type="text"
        class="form-control"
        id="address"
      />
    </div>
    <div class="col-md-6 mb-3">
      <label for="birthDate" class="form-label">birthDate</label>
      <input
        required
        v-model="form.birthDate"
        type="date"
        class="form-control"
        id="birthDate"
      />
    </div>

    <button type="submit" class="btn btn-primary">Submit</button>
  </form>
</template>

<script>
import { defineComponent } from "vue";
import axios from "axios";

export default defineComponent({
  data() {
    return {
      form: {
        firstName: null,
        familyName: null,
        fatherName: null,
        userName: null,
        password: null,
        birthDate: null,
        address: null,
        occupation: null,
      },
      status: null,
      message: "",
    };
  },
  methods: {
    createUser: function () {
      axios
        .post("api/Users", this.form)
        .then((res) => {
          this.message = res.data.message;
          this.form = {
            firstName: null,
            familyName: null,
            fatherName: null,
            userName: null,
            password: null,
            birthDate: null,
            address: null,
            occupation: null,
          };
        })
        .catch((err) => {
          this.message = Object.values(err.response.data.errors).flat().join(",") ?? err.response.data.title ?? err.response.data.message ?? "Error";
          console.log(err, err.response.data.message);
        });
    },
  },
});
</script>
