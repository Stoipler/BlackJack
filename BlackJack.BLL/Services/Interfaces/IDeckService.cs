using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Common.Models;

namespace BlackJack.BLL.Services.Interfaces
{
    public interface IDeckService
    {
        void ShuffleDeck();
        Card DrawCard();
        void InitializeNewDeck(int subdecks);
    }
}
