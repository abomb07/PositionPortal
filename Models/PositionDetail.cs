/**
 * Adam Bender
 * CST452 Mark Reha
 * 2/6/22
 * Position detail model
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PositionPortal.Models
{
    public class PositionDetail
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double Quote { get; set; }
        public double Balance { get; set; }
        public double CostBasis { get; set; }
        public double GainLoss { get; set; }
        public int Type { get; set; }
    }
}
