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
        <li v-if="positionsDetail.quote.type == 1"><a :href="'https://whalewisdom.com/stock/' + positionsDetail.quote.name" target="_blank">View Institutional Ownership</a></li>
        <li v-if="positionsDetail.quote.type == 1"><a :href="'https://senatestockwatcher.com/summary_by_ticker/' + positionsDetail.quote.name" target="_blank">View Political Ownership</a></li>
      </ul>
    </div>
    <div>
      <h3>90 Day Chart</h3>
      <trading-vue :data="this.$data" :key="positionsDetail.quote.name"></trading-vue>
    </div>
  </div>
</template>

<script>
import { mapState, mapActions } from 'vuex'
import TradingVue from 'trading-vue-js'
import {positionsDetail} from "../_store/positionsDetail.module";

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
    return {
      ohlcv: null
    }
  },
  async mounted () {
    const finnhub = require('finnhub');
    const api_key = finnhub.ApiClient.instance.authentications['api_key'];
    api_key.apiKey = "c6a6ijaad3idi8g5ovdg"
    const finnhubClient = new finnhub.DefaultApi()
    let finalArr = new Array();
    let finalDate = Math.floor(new Date().getTime() / 1000);
    let pD = new Date();
    pD.setDate(pD.getDate() - 90);
    pD = Math.floor(pD.getTime() / 1000);
    //if stock, query the stock candle api
    if (positionsDetail.state.quote.type == 1)
    {
      finnhubClient.stockCandles(positionsDetail.state.quote.name, "D", pD, finalDate, (error, defData, response) => {
        finalArr = new Array(defData.t.length)
        //api response is in incorrect format
        //this is used to rearrange the array into what is expected for the tradingvue library
        for (let i = 0; i < finalArr.length; i++)
        {
          let fooArr = [defData.t[i], defData.o[i], defData.h[i], defData.l[i], defData.c[i], defData.v[i]]
          fooArr = fooArr.map(parseFloat)
          finalArr[i] = fooArr;
        }
        this.ohlcv = finalArr;
      });
    }
    //if crypto, query the crypto candle api from the binance trade
    else
    {
      var posFoo = "BINANCE:" + positionsDetail.state.quote.name + "USDT";
      finnhubClient.cryptoCandles(posFoo, "D", pD, finalDate, (error, defData, response) => {
        finalArr = new Array(defData.t.length)
        for (let i = 0; i < finalArr.length; i++)
        {
          let fooArr = [defData.t[i], defData.o[i], defData.h[i], defData.l[i], defData.c[i], defData.v[i]]
          fooArr = fooArr.map(parseFloat)
          finalArr[i] = fooArr;
        }
        this.ohlcv = finalArr;
      });
    }
  }
};
</script>