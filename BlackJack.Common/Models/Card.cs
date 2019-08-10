using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Common.Enums;

namespace BlackJack.Common.Models
{
    public class Card
    {
        public CardValue Value { get; set; }
        public CardSuit Suit { get; set; }
    }
}
