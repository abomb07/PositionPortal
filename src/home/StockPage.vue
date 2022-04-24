<!--
Adam Bender
CST452 Mark Reha
2/6/22
Stock page
-->

<template>
  <div>
    <h1>Hi {{account.user.firstName}}!</h1>
    <h3>Here are all your open stock positions:</h3>
    <div>
      <span v-if="stocks.error" class="text-danger">ERROR: {{stocks.error}}</span>
      <ul v-if="stocks.items">
        <li v-for="cry in stocks.items" :key="cry">
          <a @click="getQuote({userid: account.user.id, quoteName: cry, quoteType: 1})">{{cry}}</a>
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
      stocks: state => state.stocks.all
    })
  },
  created () {
    this.getAll(this.account.user.id);
  },
  methods: {
    ...mapActions('stocks', {
      getAll: 'getAll',
    }),
    ...mapActions('positionsDetail', {
      getQuote: 'getQuote',
    })
  }
};
</script>