using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Common.Enums;

namespace BlackJack.Common.Models
{
    public class AuthenticationUserView
    {
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public AuthenticationType Operation { get; set; }
    }
}
