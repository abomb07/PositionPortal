<!--
Adam Bender
CST452 Mark Reha
2/6/22
crypto page
-->

<template>
  <div>
    <h1>Hi {{account.user.firstName}}!</h1>
    <h3>Here are all your open crypto positions:</h3>
    <div>
      <span v-if="crypto.error" class="text-danger">ERROR: {{crypto.error}}</span>
      <ul v-if="crypto.items">
        <li v-for="cry in crypto.items" :key="cry">
          <a @click="getQuote({userid: account.user.id, quoteName: cry, quoteType: 2})">{{cry}}</a>
        </li>
      </ul>
    </div>
    <p>
      <router-link to="/">Back</router-link>
    </p>
  </div>
</template>

<script>
import { mapState, mapActions } from 'vuex'
export default {
  computed: {
    ...mapState({
      account: state => state.account,
      crypto: state => state.crypto.all
    })
  },
  created () {
    this.getAll(this.account.user.id);
  },
  methods: {
    ...mapActions('crypto', {
      getAll: 'getAll',
    }),
    ...mapActions('positionsDetail', {
      getQuote: 'getQuote',
    })
  }
};
</script>