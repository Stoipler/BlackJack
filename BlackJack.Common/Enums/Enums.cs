using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Common.Enums
{
    public enum UserAction
    {
        Hit=1,
        Stay=2,
        NewGame=7,
        Logout=8
    }
    public enum CardValue
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13
    }
    public enum CardSuit
    {
        Hearts = 1,
        Spades = 2,
        Clubs = 3,
        Diamonds = 4
    }
    public enum AuthenticationType
    {
        Login = 1,
        Registration = 2,
        Game = 3
    }
    public enum ConsoleManagment
    {
        AddText,
        Clear
    }
}

