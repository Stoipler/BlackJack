using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.PL.Pages;
// new comment
namespace BlackJack.PL
{
    class Program
    {
        static void Main(string[] args)
        {
            StartPage _page = new StartPage();
            _page.Initialize();

            Console.ReadKey();
        }
    }
}
