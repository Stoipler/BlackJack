using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Common.Enums;
using BlackJack.Common;
using BlackJack.Common.Models;
using BlackJack.Common.Models.Base;
using BlackJack.BLL.Services.Interfaces;

namespace BlackJack.BLL.Services
{
    public class DealerService: IDealerService
    {
        private DealerModel _dealer;

        private Func<Card> _getCardFromDeck;
        private Action<BasePlayer, ConsoleManagment> _showPlayer;

        public DealerService( Func<Card> getCardFromDeck, Action<BasePlayer, ConsoleManagment> showPlayer)
        {
            _dealer = new DealerModel();
            _getCardFromDeck = getCardFromDeck;
            _showPlayer = showPlayer;
        }

        public void PrepareToNewRound()
        {
            _dealer.Cards.Clear();
            for (int i = 0; i < 2; i++)
            {
                _dealer.Cards.Add(_getCardFromDeck());
            }
        }
        public void Turn()
        {
            if (Constants.ScoreLimitForBots >= GetScore())
            {
                _dealer.Cards.Add(_getCardFromDeck());
                Turn();
                return;
            }
        }
        public void ShowPlayer()
        {
            _showPlayer(_dealer, ConsoleManagment.AddText);
        }

        public int GetScore()
        {
            return _dealer.Score;
        }
    }
}
