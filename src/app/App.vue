<!--
Adam Bender
CST452 Mark Reha
2/6/22
Base template for all .vue files. Contains navbar
-->

<template>
  <div>
    <b-navbar type="dark" variant="dark">
      <b-navbar-nav v-if="account.user">
        <b-nav-item to="/">PositionPortal</b-nav-item>
        <b-nav-item to="/addposition">Add Position</b-nav-item>
        <b-nav-item to="/stocks">Stock</b-nav-item>
        <b-nav-item to="/crypto">Crypto</b-nav-item>
        <b-nav-item to="/history">History</b-nav-item>
      </b-navbar-nav>
      <b-navbar-nav v-if="!account.user">
        <b-nav-item to="/">PositionPortal</b-nav-item>
        <b-nav-item to="/login">Login</b-nav-item>
        <b-nav-item to="/register">Register</b-nav-item>
      </b-navbar-nav>
      <b-navbar-nav class="ml-auto">
        <b-nav-item to="/login" >Logout</b-nav-item>
      </b-navbar-nav>
    </b-navbar>
    <div class="shadow p-3 mb-5 bg-white rounded">
      <div class="container">
        <div class="row">
          <div class="col-sm-6 offset-sm-3">
            <div v-if="alert.message" :class="`alert ${alert.type}`">{{alert.message}}</div>
              <router-view></router-view>
            </div>
          </div>
        </div>
      </div>
    </div>
</template>

<script>
import { mapState, mapActions } from 'vuex'

export default {
    name: 'app',
  computed: {
        ...mapState({
            alert: state => state.alert,
          account: state => state.account
        })
    },
    methods: {
        ...mapActions({
            clearAlert: 'alert/clear' 
        })
    },
    watch: {
        $route (to, from){
            // clear alert on location change
            this.clearAlert();
        }
    } 
};
</script>