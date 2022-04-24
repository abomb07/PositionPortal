<!--
Adam Bender
CST452 Mark Reha
2/6/22
Home page
-->

<template>
  <div>
    <h1>Hi {{account.user.firstName}}!</h1>
    <h3>Here are all your open positions:</h3>
    <div>
      <span v-if="positions.error" class="text-danger">ERROR: {{positions.error}}</span>
      <ul v-if="positions.items">
        <li v-for="user in positions.items" :key="user">
          {{user}}
        </li>
      </ul>
    </div>
    <h3>Summary of your account:</h3>
    <div>
      <span v-if="positionsDetail.quote.error" class="text-danger">ERROR: {{positionsDetail.quote.error}}</span>
      <ul v-if="positionsDetail.quote">
        <li>Balance: {{positionsDetail.quote.balance}}</li>
        <li>Gain/Loss: {{positionsDetail.quote.gainLoss}}</li>
      </ul>
    </div>
  </div>
</template>

<script>
import { mapState, mapActions } from 'vuex'
export default {
  computed: {
    ...mapState({
      account: state => state.account,
      positions: state => state.positions.all,
      positionsDetail: state => state.positionsDetail
    })
  },
  created () {
    this.getAll(this.account.user.id);
    this.getTotalQuote(this.account.user.id);
  },
  methods: {
    ...mapActions('positions', {
      getAll: 'getAll',
    }),
    ...mapActions('positionsDetail', {
      getTotalQuote: 'getTotalQuote',
    })
  }
};
</script>