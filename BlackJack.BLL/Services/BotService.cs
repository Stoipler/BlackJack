using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Common.Models;
using BlackJack.Common.Models.Base;
using BlackJack.Common;
using BlackJack.Common.Enums;
using BlackJack.BLL.Services.Interfaces;

namespace BlackJack.BLL.Services
{
    public class BotService:IBotService
    {
        private List<BotModel> _bots;

        private Func<Card> _getCardFromDeck;
        private Action<BasePlayer, ConsoleManagment> _showPlayer;
        private Action<string, string> _showRoundResult;

        public BotService(Func<Card> getCardFromDeck, Action<BasePlayer, ConsoleManagment> showPlayer, Action<string, string> showRoundResult)
        {
            _getCardFromDeck = getCardFromDeck;
            _showPlayer = showPlayer;
            _showRoundResult = showRoundResult;

        }

        public void PrepareToNewRound()
        {
            foreach (BotModel bot in _bots)
            {
                bot.Cards.Clear();
                for (int i = 0; i < 2; i++)
                {
                    bot.Cards.Add(_getCardFromDeck());
                }
            }
        }
        public void Turn()
        {
            foreach(BotModel bot in _bots)
            {
                BotLogic(bot);
            }
        }
        public void ShowPlayer()
        {
            foreach (BotModel bot in _bots)
            {
                _showPlayer(bot, ConsoleManagment.AddText);
            }
        }

        public void CheckRoundResult(int dealerScore)
        {
            for(int i=0; i<_bots.Count;i++)
            {
                if (Constants.BlackJack - dealerScore < 0 && Constants.BlackJack - _bots[i].Score > 0)
                {
                    _bots[i].Money += _bots[i].Bid;
                    _showRoundResult(_bots[i].Name, String.Format("{0} WON!!!", _bots[i].Name));
                    continue;
                }
                if (Constants.BlackJack - dealerScore > 0 && Constants.BlackJack - _bots[i].Score < 0)
                {
                    _bots[i].Money -= _bots[i].Bid;
                    CheckAmount(_bots[i], i);
                    _showRoundResult(_bots[i].Name, "Dealer WON!!!");
                    continue;
                }
                if (dealerScore == _bots[i].Score)
                {
                    _showRoundResult(_bots[i].Name, "DRAW!!!");
                    continue;
                }
                if (Math.Abs(Constants.BlackJack - dealerScore) < Math.Abs(Constants.BlackJack - _bots[i].Score))
                {
                    _bots[i].Money -= _bots[i].Bid;
                    CheckAmount(_bots[i], i);
                    _showRoundResult(_bots[i].Name, "Dealer WON!!!");
                    continue;
                }
                _bots[i].Money += _bots[i].Bid;
                _showRoundResult(_bots[i].Name, String.Format("{0} WON!!!", _bots[i].Name));
                
            }
        }
        private string CheckAmount(BotModel bot,int index)
        {
            if (bot.Money <= 0)
            {
                _bots.RemoveAt(index);
                return String.Format("\t{0} Left the game",bot.Name);
            }
            return Constants.EmptyString;
        }
        public void SetBotsQuantity(int numberOfBots)
        {
            _bots = new List<BotModel>();
            for (int i = 0; i < numberOfBots; i++)
            {
                _bots.Add(new BotModel(String.Format("Bot {0}", i + 1)));
            }
        }
        private void BotLogic(BotModel bot)
        {
            bot.Bid = 50;
            if (Constants.ScoreLimitForBots >= bot.Score)
            {
                bot.Cards.Add(_getCardFromDeck());
                BotLogic(bot);
                return;
            }
        }
    }
}
