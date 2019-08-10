using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Common.Enums;

namespace BlackJack.BLL.Services.Interfaces
{
    public interface IBasePlayerService
    {
        void PrepareToNewRound();
        void Turn();
        void ShowPlayer();
    }
}
