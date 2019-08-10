using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Common.Models;

namespace BlackJack.BLL.Services.Interfaces
{
    public interface IUserService:IBasePlayerService,IOrdinaryPlayer
    {
        bool IsUserLoggedOn { get; }
        string GetUsername();
        void LoginProcess(AuthenticationUserView userView);
        bool RegistrationProcess(AuthenticationUserView userView);
        void Logout();
    }
}
