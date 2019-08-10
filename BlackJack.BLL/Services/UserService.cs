using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.DAL.Entities;
using BlackJack.DAL;
using BlackJack.DAL.Inerfaces;
using BlackJack.Common.Models;
using BlackJack.Common.Models.Base;
using BlackJack.Common.Enums;
using BlackJack.Common;
using BlackJack.BLL.Services.Interfaces;

namespace BlackJack.BLL.Services
{
    public class UserService:IUserService
    {
        #region IUserService fields
        private IUserRepository _userRepository;
        private User _user;
        public bool IsUserLoggedOn { get
            {
                if (_user != null)
                {
                    return true;
                }
                return false;
            } }
        #endregion
        #region IPlayerService fields
        private UserModel _userModel;
        private Func<Card> _getCardFromDeck;
        private Func<string,  UserAction> _getUserAction;
        private Action<BasePlayer, ConsoleManagment> _showPlayer;
        private Action<string, string> _showRoundResult;
        #endregion

        public UserService(Func<Card> getCardFromDeck, Action<BasePlayer,ConsoleManagment> showPlayer, Action<string, string> showRoundResult, Func<string, UserAction> getUserAction)
        {
            _userRepository = new UserRepository();
            _userModel = new UserModel();
            _getCardFromDeck = getCardFromDeck;
            _getUserAction = getUserAction;
            _showPlayer = showPlayer;
            _showRoundResult = showRoundResult;
        }

        #region IPlayerService implementation
        public void Turn()
        {
            _userModel.Bid = 50;
            UserAction choice = _getUserAction(BLL_Messages.GameProcessInfo);
            if (choice == UserAction.Hit)
            {
                _userModel.Cards.Add(_getCardFromDeck());
                ShowPlayer();
                Turn();
                return;
            }
            if (choice == UserAction.Stay)
            {
                return;
            }
            ShowPlayer();
            Turn();
        }
        public void PrepareToNewRound()
        {
            _userModel.Name = _user.Userame;
            _userModel.Bid = 50;
            _userModel.Cards.Clear();
            for (int i = 0; i < 2; i++)
            {
                _userModel.Cards.Add(_getCardFromDeck());
            }
            ShowPlayer();
        }
        public void ShowPlayer()
        {
            _showPlayer(_userModel,ConsoleManagment.Clear);
        }

        public int GetScore()
        {
            return _userModel.Score;
        }
        public void CheckRoundResult(int dealerScore)
        {
            if (Constants.BlackJack - dealerScore < 0 && Constants.BlackJack - _userModel.Score > 0)
            {
                _showRoundResult(_user.Userame, String.Format("{0} WON!!!", _user.Userame));
                _userModel.Money += _userModel.Bid;
                return;
            }
            if (Constants.BlackJack - dealerScore > 0 && Constants.BlackJack - _userModel.Score < 0)
            {
                _showRoundResult(_user.Userame, "Dealer WON!!!");
                _userModel.Money -= _userModel.Bid;
                return;
            }
            if (dealerScore == _userModel.Score)
            {
                _showRoundResult(_user.Userame, "DRAW!!!");
                return;
            }
            if (Math.Abs(Constants.BlackJack - dealerScore) < Math.Abs(Constants.BlackJack - _userModel.Score))
            {
                _showRoundResult(_user.Userame, "Dealer WON!!!");
                _userModel.Money -= _userModel.Bid;
                return;
            }
            _showRoundResult(_user.Userame, String.Format("{0} WON!!!", _user.Userame));
            _userModel.Money += _userModel.Bid;
        }
        #endregion
        #region IUserService implementation
        public void LoginProcess(AuthenticationUserView userView)
        {
            _user=_userRepository.GetUser(userView.Username, userView.UserPassword);
        }
        public bool RegistrationProcess(AuthenticationUserView userView)
        {
            return _userRepository.CreateNewUser(userView.Username, userView.UserPassword);    
        }
        public string GetUsername()
        {
            return _user.Userame;
        }
        public void Logout()
        {
            _user = null;
        }
        #endregion

    }
}
