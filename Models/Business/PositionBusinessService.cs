/**
 * Adam Bender
 * CST452 Mark Reha
 * 2/6/22
 * Position Business service
 **/

using Newtonsoft.Json;
using PositionPortal.Helpers;
using PositionPortal.Models.Data;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PositionPortal.Models.Business
{
    public class PositionBusinessService
    {
        PositionDataService pds = new PositionDataService();

        public bool Insert(Position position)
        {
            return pds.Insert(position);
        }
        public List<Position> FindByUserId(int UserId)
        {
            return pds.FindByUserId(UserId);
        }

        public bool Update(Position position)
        {
            return pds.Update(position);
        }

        public bool Delete(int Id)
        {
            return pds.Delete(Id);
        }

        public List<Position> FindAllCurPos(int UserId)
        {
            //this block adds the quantitys of all the users positions to find if they are still open or closed
            //quantity > 0 means the position is still open
            var groupedByName = FindByUserId(UserId).ToLookup(x => x.Name);
            var totalQuantity = groupedByName.ToDictionary(
                x => x.Key,
                x => x.Sum(x => x.Quantity)
            );

            var res = FindByUserId(UserId).Where(z => totalQuantity[z.Name] != 0).ToList();
            return res;
        }

        public List<Position> FindAllPastPos(int UserId)
        {
            //this block adds the quantitys of all the users positions to find if they are still open or closed
            //quantity < 0 means the position is still open
            var groupedByName = FindByUserId(UserId).ToLookup(x => x.Name);
            var totalQuantity = groupedByName.ToDictionary(
                x => x.Key,
                x => x.Sum(x => x.Quantity)
            );

            var res = FindByUserId(UserId).Where(z => totalQuantity[z.Name] == 0).ToList();
            return res;
        }

        public PositionDetail GetRealizedData(int UserId)
        {
            PositionDetail res = new PositionDetail();
            List<Position> foo = FindAllPastPos(UserId);
            double plus = 0;
            double neg = 0;

            foreach (var item in foo)
            {
                //a quantity greater than 0 represents a buy
                //less than zero represents a sell
                if (item.Quantity > 0)
                {
                    neg += item.Price * item.Quantity;
                }else
                {
                    plus += Math.Abs(item.Quantity) * item.Price;
                }
            }
            res.GainLoss = Math.Round(plus - neg, 2);
            return res;
        }

        /// <summary>
        /// For home page
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<string> FindAllCurNames(int UserId)
        {
            //list of open positions with no duplicates
            return FindAllCurPos(UserId).Select(s => s.Name).Distinct().ToList();
        }

        /// <summary>
        /// For Stock page
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<string> FindAllCurStock(int UserId)
        {
            //list of open stock positions with no duplicates
            return FindAllCurPos(UserId).Where(z => z.Type == 1).Select(s => s.Name).Distinct().ToList();
        }

        /// <summary>
        /// For Crypto page
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<string> FindAllCurCrypto(int UserId)
        {
            //list of open crypto positions with no duplicates
            return FindAllCurPos(UserId).Where(z => z.Type == 2).Select(s => s.Name).Distinct().ToList();
        }

        /// <summary>
        /// For detail page
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="UserId"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public PositionDetail GetPosDetail(string Name, int UserId, int Type)
        {
            PositionDetail res = new PositionDetail();
            List<Position> allPos = FindAllCurPos(UserId).Where(s => s.Name == Name).ToList();
            double purchasePrice = 0;
            double count = 0;

            //add quantity and price from all positions
            foreach (var item in allPos)
            {
                res.Quantity += item.Quantity;
                if (item.Quantity > 0)
                {
                    purchasePrice += item.Price * item.Quantity;
                    count += item.Quantity;
                }
            }
            //average price per share
            double avgPrice = purchasePrice / count;
            //cost basis calculation
            res.CostBasis = Math.Round(res.Quantity * avgPrice, 2);

            res.Name = Name;
            res.Quote = Type == 1 ? ApiHelper.GetStockQuote(Name) : ApiHelper.GetCryptoQuote(Name);
            res.Balance = Math.Round(res.Quantity * res.Quote, 2);
            res.GainLoss = Math.Round(res.Balance - res.CostBasis, 2);
            res.Type = Type;
            return res;
        }

        /// <summary>
        /// For home page
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public PositionDetail GetTotalDetail(int userid)
        {
            PositionDetail res = new PositionDetail();
            List<Position> allPos = FindAllCurPos(userid).GroupBy(s => s.Name).Select(x => x.First()).ToList();

            foreach (var item in allPos)
            {
                //get the balance and gainloss of all position to calculate the total balance for the account
                PositionDetail foo = GetPosDetail(item.Name, userid, item.Type);
                res.Balance += foo.Balance;
                res.GainLoss += foo.GainLoss;
            }
            res.Balance = Math.Round(res.Balance, 2);
            res.GainLoss = Math.Round(res.GainLoss, 2);
            return res;
        }
    }
}
