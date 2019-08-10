using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Common.Models.Base;
using BlackJack.Common.Models;

namespace BlackJack.Common.Models
{
    public class DealerModel:BasePlayer
    {
        public DealerModel()
        {
            Name = "Dealer";
            Cards = new List<Card>();
        }
    }
}
