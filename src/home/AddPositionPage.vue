<!--
Adam Bender
CST452 Mark Reha
2/6/22
Add Position page
-->

<template>
  <div>
    <h2 style="text-align: center;">Add Position</h2>
    <form @submit.prevent="handleSubmit">
      <div class="form-group">
        <label for="name">Name</label>
        <input type="text" v-model="position.name" v-validate="'required'" name="name" class="form-control" :class="{ 'is-invalid': submitted && errors.has('name') }" />
        <div v-if="submitted && errors.has('name')" class="invalid-feedback">{{ errors.first('name') }}</div>
      </div>
      <div class="form-group">
        <label for="type">Type</label>
        <select v-model="position.type" v-validate="'required'" name="type" class="form-control" :class="{ 'is-invalid': submitted && errors.has('type') }">
          <option value="1">Stock</option>
          <option value="2">Crypto</option>
        </select>
        <div v-if="submitted && errors.has('type')" class="invalid-feedback">{{ errors.first('type') }}</div>
      </div>
      <div class="form-group">
        <label for="quantity">Quantity (enter negative number for a sell)</label>
        <input type="text" v-model="position.quantity" v-validate="{ required: true }" name="quantity" class="form-control" :class="{ 'is-invalid': submitted && errors.has('quantity') }" />
        <div v-if="submitted && errors.has('quantity')" class="invalid-feedback">{{ errors.first('quantity') }}</div>
      </div>
      <div class="form-group">
        <label for="price">Price</label>
        <input type="text" v-model="position.price" v-validate="{ required: true }" name="price" class="form-control" :class="{ 'is-invalid': submitted && errors.has('price') }" />
        <div v-if="submitted && errors.has('price')" class="invalid-feedback">{{ errors.first('price') }}</div>
      </div>
      <div class="form-group">
        <label for="date">Date</label>
        <input type="date" v-model="position.date" v-validate="{ required: true }" name="date" class="form-control" :class="{ 'is-invalid': submitted && errors.has('date') }" />
        <div v-if="submitted && errors.has('date')" class="invalid-feedback">{{ errors.first('date') }}</div>
      </div>
      <div class="form-group">
        <button class="btn btn-primary" >Save</button>
        <router-link to="/" class="btn btn-link">Cancel</router-link>
      </div>
    </form>
  </div>
</template>

<script>
import { mapState, mapActions } from 'vuex'
import moment from 'moment'

export default {
  data () {
    return {
      position: {
        name: '',
        type: '',
        quantity: '',
        price: '',
        date: '',
        userId: ''
      },
      submitted: false
    }
  },
  computed: {
    ...mapState('positions', ['status']),
    ...mapState({account: state => state.account})
  },
  methods: {
    ...mapActions('positions', ['insert']),
    handleSubmit(e) {
      this.submitted = true;
      this.$validator.validate().then(valid => {
        if (valid) {
          this.position.userId = this.account.user.id;
          this.insert(this.position);
        }
      });
    }
  }
};
</script>