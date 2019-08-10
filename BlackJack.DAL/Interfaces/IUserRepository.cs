using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.DAL.Entities;

namespace BlackJack.DAL.Inerfaces
{
    public interface IUserRepository
    {
        bool CreateNewUser(string username, string password);
        User GetUser(string username, string password);
    }
}
