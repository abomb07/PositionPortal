<!--
Adam Bender
CST452 Mark Reha
2/6/22
Detail and Chart page
-->

<template>
  <div>
    <div>
      <span v-if="positionsDetail.quote.error" class="text-danger">ERROR: {{positionsDetail.quote.error}}</span>
      <ul v-if="positionsDetail.quote">
        <li>Name: {{positionsDetail.quote.name}}</li>
        <li>Quantity: {{positionsDetail.quote.quantity}}</li>
        <li>Quote: {{positionsDetail.quote.quote}}</li>
        <li>Balance: {{positionsDetail.quote.balance}}</li>
        <li>Cost Basis: {{positionsDetail.quote.costBasis}}</li>
        <li>Gain/Loss: {{positionsDetail.quote.gainLoss}}</li>
      </ul>
    </div>
    <div>
      <trading-vue :data="this.$data"></trading-vue>
    </div>
    <p>
      <router-link to="/addposition">Add More</router-link>
    </p>
    <p>
      <router-link to="/stocks">View Stocks</router-link>
    </p>
    <p>
      <router-link to="/crypto">View Crypto</router-link>
    </p>
    <p>
      <router-link to="/login">Logout</router-link>
    </p>
  </div>
</template>

<script>
import { mapState, mapActions } from 'vuex'
import TradingVue from 'trading-vue-js'
import finnhub from "finnhub";
export default {
  computed: {
    ...mapState({
      account: state => state.account,
      positionsDetail: state => state.positionsDetail
    })
  },
  name: 'app',
  components: { TradingVue },
  data() {
    let stockData;
    const finnhub = require('finnhub');
    const api_key = finnhub.ApiClient.instance.authentications['api_key'];
    api_key.apiKey = "c6a6ijaad3idi8g5ovdg"
    const finnhubClient = new finnhub.DefaultApi()
    var finalArr;
    finnhubClient.stockCandles("AAPL", "D", 1590988249, 1591852249, (error, defData, response) => {
      finalArr = new Array(9)
      //let foo = JSON.parse(JSON.stringify(defData));
      for (let i = 0; i < finalArr.length; i++)
      {
        let fooArr = [defData.t[i], defData.o[i], defData.h[i], defData.l[i], defData.c[i], defData.v[i]]
        fooArr = fooArr.map(parseFloat)
        finalArr[i] = fooArr;
      }
      finalArr = JSON.stringify(finalArr)
      console.log(finalArr)
    });

    return {
      ohlcv: [
        [ 1551128400000, 33,  37.1, 14,  14,  196 ],
        [ 1551132000000, 13.7, 30, 6.6,  30,  206 ],
        [ 1551135600000, 29.9, 33, 21.3, 21.8, 74 ],
        [ 1551139200000, 21.7, 25.9, 18, 24,  140 ],
        [ 1551142800000, 24.1, 24.1, 24, 24.1, 29 ],
      ]
    }
  }
};
</script>