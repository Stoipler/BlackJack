using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Common.Models.Base;

namespace BlackJack.Common.Models
{
    public class BotModel:BasePlayer
    {
        public BotModel(string name)
        {
            Name = name;
            Cards = new List<Card>();
        }
    }
}
