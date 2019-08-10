using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Common.Enums;
using BlackJack.Common.Models.Base;

namespace BlackJack.Common.Models
{
    public class PresentationModel
    { 
        public Func<string, AuthenticationUserView> getUserView;
        public Action<BasePlayer,ConsoleManagment> showPlayer;
        public Func<string, UserAction> getUserAction;
        public Func<string,string, int> getGameSettings;
        public Action<string ,string> showRoundResult;
        public Action readKey;

        public PresentationModel(Func<string, AuthenticationUserView> getUserView, Action<BasePlayer,ConsoleManagment> showPlayer,
            Func<string, UserAction> getUserAction, Func<string,string, int> getGameSettings, Action<string,string> showRoundResult, Action readKey)
        {
            this.getUserView=getUserView;
            this.showPlayer=showPlayer;
            this.getUserAction=getUserAction;
            this.getGameSettings=getGameSettings;
            this.showRoundResult=showRoundResult;
            this.readKey = readKey;
        }
    }
}
