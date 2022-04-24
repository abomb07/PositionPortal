<!--
Adam Bender
CST452 Mark Reha
2/6/22
Position history page
-->

<template>
  <div>
    <h1>Hi {{account.user.firstName}}!</h1>
    <h3>Summary of your closed positions:</h3>
    <div>
      <span v-if="positionsDetail.quote.error" class="text-danger">ERROR: {{positionsDetail.quote.error}}</span>
      <ul v-if="positionsDetail.quote">
        <li>Realized Gain/Loss: {{positionsDetail.quote.gainLoss}}</li>
      </ul>
    </div>
    <h3>Here are all your closed positions:</h3>
    <div>
      <span v-if="positions.error" class="text-danger">ERROR: {{positions.error}}</span>
      <table id="table_id" class="display" style="width:100%">
        <thead>
        <tr>
          <th style="width:25%">Name</th>
          <th style="width:15%">Type</th>
          <th style="width:15%">Quantity</th>
          <th style="width:15%">Price</th>
          <th style="width:30%">Date</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="pos in positions.items">
          <td>{{pos.name}}</td>
          <td><div v-if="pos.type == 1">Stock</div><div v-if="pos.type == 2">Crypto</div></td>
          <td>{{pos.quantity}}</td>
          <td>{{pos.price}}</td>
          <td>{{pos.date.split('T')[0]}}</td>
        </tr>
        </tbody>
      </table>
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
  watch: {
    positions() {
      $('#table_id').DataTable().destroy();
      this.$nextTick(()=> {
        $('#table_id').DataTable()
      });
    }
  },
  created () {
    this.getPast(this.account.user.id);
    this.getRealized(this.account.user.id);
  },
  methods: {
    ...mapActions('positions', {
      getPast: 'getPast',
    }),
    ...mapActions('positionsDetail', {
      getRealized: 'getRealized',
    })
  }
};
</script>