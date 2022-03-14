/**
 * Adam Bender
 * CST452 Mark Reha
 * 2/6/22
 * Position model
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PositionPortal.Models
{
    public class Position
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }

        public Position()
        {

        }

        public Position(int id, int type, string name, double quantity, double price, DateTime date, int userId)
        {
            Id = id;
            Type = type;
            Name = name;
            Quantity = quantity;
            Price = price;
            Date = date;
            UserId = userId;
        }
    }
}
