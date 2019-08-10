using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Common
{
    public class Constants
    {
        public const string StartPageHeader = "START PAGE\n" +
            "Enter \"1\" if you want to login\n" +
            "Enter \"2\" if you want to create new user";
        public const string RegistrationFormHeader = "REGISTRATION PAGE";
        public const string LoginFormHeader = "LOGIN PAGE";

        public const string NameRequest = "Please, enter username:";
        public const string PasswordRequest = "Please, enter password:";
        public const string PasswordConfirmationRequest = "Please, confirm your password:";
        public const string EmptyUsernameField = "You didn't enter username, try again";
        public const string EmptyPasswordField = "You didn't enter password, try again";
        public const string DifferentPasswords = "Passwords must be same, try again";

        public const string EmptyString = "";
        public const string UsernamePattern = @"^(?=.*[A-Z]).{4,15}$";
        public const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,15}$";
        public const string WrongUsernameFormatMessage = "Username must contain at least 4 characters";
        public const string WrongPasswordFormatMessage = "Password must contain next characters :\n" +
                                                         "A-Z, a-z, 0-9 and have length not smaller than 6 characters";

        public const int BlackJack = 21;
        public const int ScoreLimitForBots = 17;
        public const string NumberOfDecksRequest = "How much decks do you want to use in game? (1-8 decks):";
        public const int DownLimitOfDecks = 1;
        public const int UpLimitOfDecks = 8;

        public const string NumberOfRoundRequest = "How much rounds do you want to play? (4-20 rounds):";
        public const int DownLimitOfRounds = 4;
        public const int UpLimitOfRounds = 20;

        public const string NumberOfBotsRequest = "How much bots do you want to add? (0-4 bots):";
        public const int DownLimitOfBots = 0;
        public const int UpLimitOfBots = 4;
    }
    public class BLL_Messages
    {
        public const string LoginMessage_Success = "Sign up process is sucsessful";
        public const string LoginMessage_Failure = "Wrong username or password, please try again";

        public const string RegistrationMessage_Success = "New user was created";
        public const string RegistrationMessage_Failure = "User with this username already exsists, please try again";

        public const string WrongEnteredValue = "Entered value mustn't be smaller than {0} and bigger than {1}";
        public const string GameOverWarning = "You should press the keys 'Y' or 'N' to choose option";


        public const string GameProcessInfo = "Press '1' for Hit or '2' for Stay";

        public const string AfterGameOverMessage = "\nDo you want to logout? If yes press 'Y' or press 'N' to start new game";

    }
    
}
