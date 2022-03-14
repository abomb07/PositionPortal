/**
 * Adam Bender
 * CST452 Mark Reha
 * 2/6/22
 * 3rd party api helper class
 **/

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PositionPortal.Helpers
{
    public static class ApiHelper
    {
        private class StockData
        {
            public double c { get; set; }
        }

        public static double GetStockQuote(string Name)
        {
            var apiKey = "c6a6ijaad3idi8g5ovdg";
            var url = $"https://finnhub.io/api/v1/quote?symbol={Name}&token={apiKey}";
            var json = "";

            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString(url);
            }
            //deserialize into object where current cost is 'c'
            var data = JsonConvert.DeserializeObject<StockData>(json);

            //return curent cost rounded to 2nd decimal
            return Math.Round(data.c, 2);
        }

        private class CryptoData
        {
            public double price { get; set; }
        }

        public static double GetCryptoQuote(string Name)
        {
            var apiKey = "56fd5e83-89b8-4cf9-a2ca-04df58d63c43";
            var url = $"https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest?convert=USD&symbol={Name}";
            var json = "";

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("X-CMC_PRO_API_KEY", apiKey);
                json = wc.DownloadString(url);
            }
            //deserialize into object where current cost is 'price'
            JObject jobj = JObject.Parse(json);
            var data = jobj.SelectToken("data").SelectToken(Name).SelectToken("quote").SelectToken("USD").ToObject<CryptoData>();

            //return curent cost rounded to 2nd decimal
            return Math.Round(data.price, 2);
        }
    }
}
