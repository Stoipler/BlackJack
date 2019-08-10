using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Common;
using BlackJack.Common.Models;
using BlackJack.Common.Models.Base;
using BlackJack.Common.Enums;
using BlackJack.BLL;
using System.Text.RegularExpressions;

namespace BlackJack.PL.Pages
{
    public class StartPage
    {
        private PresentationModel _delegates;

        public StartPage()
        {
            _delegates = new PresentationModel(GetUserView, ShowPlayer, GetUserAction, GetGameSettings, ShowRoundResult,ReadKey);
            Console.BufferWidth = Console.WindowWidth = 70;
            Console.BufferHeight = Console.WindowHeight = 26;
            Console.CursorVisible = false;
        }

        public void Initialize()
        {
            Game game = new Game( _delegates);
            game.Start();
        }

        private AuthenticationUserView GetUserView(string message=Constants.EmptyString)
        {
            Console.Clear();
            if (!String.IsNullOrEmpty(message))
            {
                InfoMessage(message);
            }
            Console.WriteLine(Constants.StartPageHeader);
           
            var input = Console.ReadLine();
            int choice=0;
            if (Int32.TryParse(input, out choice))
            {
                if ((AuthenticationType)choice == AuthenticationType.Login)
                {
                    return LoginForm();
                }
                if ((AuthenticationType)choice == AuthenticationType.Registration)
                {
                    return RegistrationForm();
                }
            }
            return GetUserView();
        }
        private void ShowPlayer(BasePlayer player, ConsoleManagment console=ConsoleManagment.AddText)
        {
            if (console == ConsoleManagment.Clear)
            {
                Console.Clear();
            }
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine("{0}'s hand {1}:", player.Name, player.Score);
            if (player.Name != "Dealer")
            {
                Console.WriteLine("{0}'s money {1}$\t bid {2}$:", player.Name, player.Money,player.Bid);
            }
            Console.ResetColor();
            Console.WriteLine("\n{0}'s cards: ", player.Name);
            foreach (Card card in player.Cards)
            {
                Console.Write("{0}.{1} ", card.Value, card.Suit);
            }
            Console.WriteLine();
        }
        private UserAction GetUserAction(string message)
        {
            if(message==BLL_Messages.AfterGameOverMessage)
            {
                Console.Clear();
            }
            Console.WriteLine();
            InfoMessage(message,ConsoleColor.DarkYellow,ConsoleColor.Black);
            var input = Console.ReadKey();
                if (input.KeyChar=='1')
                {
                    return UserAction.Hit;
                }
                if (input.KeyChar =='2')
                {
                    return UserAction.Stay;
                }
                if (input.KeyChar =='N'|| input.KeyChar =='n')
                {
                    return UserAction.NewGame;
                }
                if (input.KeyChar =='Y'|| input.KeyChar =='y')
                {
                    return UserAction.Logout;
                }
            
            InfoMessage("Wrong key, try again");
            return GetUserAction(message);
        }
        private int GetGameSettings(string message, string warning = Constants.EmptyString)
        {
            Console.Clear();
            if (!String.IsNullOrEmpty(warning))
            {
                InfoMessage(warning);
            }
            Console.WriteLine(message);

            var input = Console.ReadLine();
            int choice = 0;
            if (Int32.TryParse(input, out choice))
            {
                return choice;
            }
            return GetGameSettings(message);
        }
        private void ShowRoundResult(string playerName,string message)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Dealer VS {0} :",  playerName);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("\t"+message);
            Console.ResetColor();
        }
        private void InfoMessage(string message, ConsoleColor background=ConsoleColor.Red, ConsoleColor foreground=ConsoleColor.Gray)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private AuthenticationUserView LoginForm(string message = Constants.EmptyString)
        {
            var userView = FillInForm(Constants.LoginFormHeader);
            userView.Operation = AuthenticationType.Login;
            return userView;
        }
        private AuthenticationUserView RegistrationForm(string message = Constants.EmptyString)
        {
            var userView = FillInForm(Constants.RegistrationFormHeader, message);

            Console.WriteLine(Constants.PasswordConfirmationRequest);
            var passwordConfirmation = HidePassword();
            if (!Regex.IsMatch(passwordConfirmation, Constants.PasswordPattern))
            {
                return RegistrationForm(Constants.WrongPasswordFormatMessage);
            }
            if (userView.UserPassword != passwordConfirmation)
            {
                return RegistrationForm(Constants.DifferentPasswords);
            }

            userView.Operation = AuthenticationType.Registration;
            return userView;
        }
        private AuthenticationUserView FillInForm(string pageHeader, string message = Constants.EmptyString)
        {
            Console.Clear();
            if (!String.IsNullOrEmpty(message))
            {
                InfoMessage(message);
            }
            Console.WriteLine(pageHeader);
            

            Console.WriteLine(Constants.NameRequest);
            var username = Console.ReadLine();
            if (!Regex.IsMatch(username, Constants.UsernamePattern, RegexOptions.IgnoreCase))
            {
                return FillInForm(pageHeader, Constants.WrongUsernameFormatMessage);
            }

            Console.WriteLine(Constants.PasswordRequest);
            var password = HidePassword();
            if (!Regex.IsMatch(password, Constants.PasswordPattern))
            {
                return FillInForm(pageHeader, Constants.WrongPasswordFormatMessage);
            }

            return new AuthenticationUserView { Username = username, UserPassword = password };
        } 
        private void ReadKey()
        {
            InfoMessage("Press any key to continue...",ConsoleColor.DarkYellow,ConsoleColor.Black);
            Console.ReadKey();
        }
        private void ClearToEndOfCurrentLine(int numberOFStringToDelete)
        {
            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop-numberOFStringToDelete;
            Console.Write(new String(' ', Console.WindowWidth - currentLeft));
            Console.SetCursorPosition(currentLeft, currentTop);

        }
        private string HidePassword(string password=Constants.EmptyString)
        {
            ConsoleKeyInfo info = Console.ReadKey(true);
            
            if (info.Key != ConsoleKey.Backspace && info.Key != ConsoleKey.Enter)
            {
                Console.Write("*");
                password += info.KeyChar;
                return HidePassword(password);
            }
            if (info.Key == ConsoleKey.Backspace)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    password = password.Substring(0, password.Length - 1);
                    int cursorPosition = Console.CursorLeft;
                    Console.SetCursorPosition(cursorPosition - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(cursorPosition - 1, Console.CursorTop);
                    return HidePassword(password);
                }
            }
            
                Console.WriteLine();
                return password;
        }
    }
}
