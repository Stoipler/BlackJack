﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BLL.Services.Interfaces
{
    public interface IBotService:IBasePlayerService,IOrdinaryPlayer
    {
        void SetBotsQuantity(int numberOfBots);
    }
}
