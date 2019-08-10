using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Common.Models;
using BlackJack.Common.Enums;
using BlackJack.Common;
using BlackJack.BLL.Services;
using BlackJack.BLL.Services.Interfaces;
using BlackJack.DAL.Entities;

namespace BlackJack.BLL
{
    public class Game
    {
        private readonly IDeckService _deck;
        private readonly IUserService _user;
        private readonly IDealerService _dealer;
        private readonly IBotService _bots;
        private AuthenticationUserView _userView;
        private PresentationModel _presentationModel;

        public Game( PresentationModel presentationModel)
        {
            _presentationModel = presentationModel;

            _deck = new DeckService();
            _user = new UserService(_deck.DrawCard,_presentationModel.showPlayer,_presentationModel.showRoundResult, _presentationModel.getUserAction);
            _bots = new BotService(_deck.DrawCard, _presentationModel.showPlayer,_presentationModel.showRoundResult);
            _dealer = new DealerService(_deck.DrawCard, _presentationModel.showPlayer);
            
        }


        public void Start()
        {
            UserDataProcessing();

            var numberOfDecks = SetNumber(Constants.NumberOfDecksRequest, Constants.DownLimitOfDecks, Constants.UpLimitOfDecks);
            var numberOfRounds = SetNumber(Constants.NumberOfRoundRequest, Constants.DownLimitOfRounds, Constants.UpLimitOfRounds);
            var numberOfBots = SetNumber(Constants.NumberOfBotsRequest, Constants.DownLimitOfBots, Constants.UpLimitOfBots);
            _bots.SetBotsQuantity(numberOfBots);
            
            for (int i = 0; i < numberOfRounds; i++)
            {
                _deck.InitializeNewDeck(numberOfDecks);
                _deck.ShuffleDeck();
                InRoundActions(_user);
                InRoundActions(_bots);
                InRoundActions(_dealer);

                ShowCards(_user);
                ShowCards(_dealer);
                ShowCards(_bots);

                CheckRoundResult(_user);
                CheckRoundResult(_bots);
                _presentationModel.readKey();
            }
            GameOverAction();
        }
        private void InRoundActions(IBasePlayerService player)
        {
            player.PrepareToNewRound();
            player.Turn();
        }
        private void ShowCards(IBasePlayerService player)
        {
            player.ShowPlayer();
        }
        private void CheckRoundResult(IOrdinaryPlayer player)
        {
            player.CheckRoundResult(_dealer.GetScore());
        }
        private void GameOverAction()
        {
            var choice = _presentationModel.getUserAction(BLL_Messages.AfterGameOverMessage);
            if (choice == UserAction.Logout)
            {
                _user.Logout();
                _presentationModel.getUserView(Constants.EmptyString);
                Start();
                return;
            }
            if (choice == UserAction.NewGame)
            {
                Start();
                return;
            }
            GameOverAction();
        }
        private void UserDataProcessing()
        {
            if (_userView==null)
            {
                _userView = _presentationModel.getUserView(Constants.EmptyString);
                UserDataProcessing();
                return;
            }
            if (_userView.Operation == AuthenticationType.Login)
            {
                _user.LoginProcess(_userView);
                if (!_user.IsUserLoggedOn)
                {
                    _userView = _presentationModel.getUserView(BLL_Messages.LoginMessage_Failure);
                    UserDataProcessing();
                    return;
                }
                return;
            }
            var registrationResult = _user.RegistrationProcess(_userView);
            if (!registrationResult)
            {
                _userView = _presentationModel.getUserView(BLL_Messages.RegistrationMessage_Failure);
                UserDataProcessing();
                return;
            }
            _userView.Operation = AuthenticationType.Login;
            UserDataProcessing();
        } 
        private int SetNumber(string message, int downLimit, int upLimit, string warning = Constants.EmptyString)
        {
            var number = _presentationModel.getGameSettings(message, warning);
            if (number > upLimit || number < downLimit)
            {
                return SetNumber(message, downLimit, upLimit, String.Format(BLL_Messages.WrongEnteredValue, downLimit, upLimit));
            }
            return number;
        }
    }
}
