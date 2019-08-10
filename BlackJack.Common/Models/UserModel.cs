using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Common.Models;
using BlackJack.Common.Models.Base;

namespace BlackJack.Common.Models
{
    public class UserModel:BasePlayer
    {
        public UserModel()
        {
            Name = "Unidentified user";
            Cards = new List<Card>();
        }
    }
}
