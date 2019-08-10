using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Common.Enums;

namespace BlackJack.Common.Models.Base
{
    public abstract class BasePlayer
    {
        public string Name;
        public List<Card> Cards;
        public int Money=150;
        public int Bid=0;
        public int Score
        {
            get
            {
                int score = 0;
                foreach (Card card in Cards)
                {
                    if (card.Value == CardValue.Jack || card.Value == CardValue.Queen || card.Value == CardValue.King)
                    {
                        score += 10;
                    }
                    if (card.Value == CardValue.Ace)
                    {
                        if (score < Constants.BlackJack)
                        {
                            score += 11;
                            continue;
                        }
                        score += 1;
                    }
                    if (card.Value != CardValue.Jack && card.Value != CardValue.Queen && card.Value != CardValue.King && card.Value != CardValue.Ace)
                    {
                        score += (int)card.Value;
                    }
                }
                return score;
            }
        }
    }
}
