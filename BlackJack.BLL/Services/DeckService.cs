using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL;
using BlackJack.Common.Models;
using BlackJack.Common.Enums;
using BlackJack.BLL.Services.Interfaces;

namespace BlackJack.BLL.Services
{
    public class DeckService:IDeckService
    {
        Deck _deck;
        Random _rnd;
        public DeckService()
        {
            _deck = new Deck();
            _rnd = new Random();
        }
        public void ShuffleDeck()
        {
            for (int shuffle = 0; shuffle < 5; shuffle++)
            {
                for (int i = 0; i < _deck.Cards.Count; i++)
                {
                    Card tmp = _deck.Cards[i];
                    _deck.Cards.RemoveAt(i);
                    _deck.Cards.Insert(_rnd.Next(_deck.Cards.Count), tmp);
                }
            }
        }
        public Card DrawCard()
        {
            Card _card;
            _card = _deck.Cards[_deck.Cards.Count - 1];
            _deck.Cards.RemoveAt(_deck.Cards.Count - 1);
            return _card;
        }
        public void InitializeNewDeck(int subdecks)
        {
            _deck = new Deck();
            for (int i = 1; i <= subdecks; ++i)
            {
                for (int j = 1; j <= 4; ++j)
                {
                    for (int k = 1; k <= 13; ++k)
                    {
                        _deck.Cards.Add(new Card { Suit = (CardSuit)j, Value = (CardValue)k });
                    }
                }
            }
        }
    }
}
