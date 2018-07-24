using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Mastermind
{
    class Program
    {
        const int MINNUMBER = 1;
        const int MAXNUMBER = 6;
        
        static void Main(string[] args)
        {
            int numberOfGuesses = 10;
            while (numberOfGuesses != 0)
            {
                if (numberOfGuesses == 10)
                {
                    Console.WriteLine("Let's play a simpler version of the game Mastermind.");
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("Instructions : Choose your 4 digit combination ranging from 1 to 6.");
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("You have 10 attempts to guess the 4 digit combination. Good luck!");
                    System.Threading.Thread.Sleep(2000);
                }
                else
                {
                    Console.WriteLine("You have " + numberOfGuesses + " attempts to guess the 4 digit combination. Good luck!");
                    System.Threading.Thread.Sleep(2000);
                }
                
                int[] randomGeneratedAnswer = RandomGeneratedAnswer(); 
                int[] player4digitcombination = UsersCombination();
                
                List<int> listOfMatchingDigits = GetMatchingDigits(player4digitcombination, randomGeneratedAnswer);
                Console.WriteLine(MatchingDigits(listOfMatchingDigits));
                var congrats = MatchingDigits(listOfMatchingDigits);
                System.Threading.Thread.Sleep(2000);
                
                if (listOfMatchingDigits.Count > 0)
                {
                    Console.WriteLine(IncorrectDigits(listOfMatchingDigits));
                    System.Threading.Thread.Sleep(2000);
                }

                numberOfGuesses--;

                if (numberOfGuesses == 10 || congrats.Equals("Congratulations you have won!"))
                {
                    if (ContinuePlaying())
                    {
                        Console.Clear();
                        numberOfGuesses = 10;
                        continue;
                    }
                    break;
                }
            }
        }

        #region Helpers
        private static int[] UsersCombination()
        {
            Console.WriteLine("Please enter your 4 digit combination.\n");
            System.Threading.Thread.Sleep(2000);
            var userdigits = Console.ReadLine();
            int[] user4digit = null;

            try
            {

                if (CheckForLettersAndSpecialCharacters(userdigits))
                {
                    user4digit = ValidationUserDigits(userdigits);
                    if (user4digit.Length == 4)
                    {
                        for (int i = 1; i < user4digit.Length; i++)
                        {
                            int x = user4digit[i];
                            if (x < MINNUMBER || x > MAXNUMBER)
                            {
                                throw new ArgumentException();
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch 
            {
                Console.WriteLine("\nAll four digits should be integers and none greater than 6.");
                System.Threading.Thread.Sleep(1500);
                Console.WriteLine("\nEnter another combination.");
                userdigits = Console.ReadLine();

                if (CheckForLettersAndSpecialCharacters(userdigits))
                {
                    user4digit = ValidationUserDigits(userdigits);
                    if (user4digit.Length == 4)
                    {
                        for (int i = 1; i < user4digit.Length; i++)
                        {
                            int x = user4digit[i];
                            if (i < MINNUMBER || i > MAXNUMBER)
                            {

                                throw new ArgumentException(); 
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nAll four digits should be integers and none greater than 6.");
                    System.Threading.Thread.Sleep(1500);
                    return UsersCombination();
                }
            }
            
            return user4digit;
        }

        private static int [] ValidationUserDigits(string userDigits)
        {
            int[] userArray = null; 
            try
            {
                userDigits.ToCharArray();
                var userdigitsList = new List<char>(userDigits);
                userArray = userdigitsList.ConvertAll(x => Int32.Parse(x.ToString())).ToArray();
            }
            catch
            {
                Console.WriteLine("\nAll four digits should be integers and none greater than 6.");
                System.Threading.Thread.Sleep(1500);
                Console.WriteLine("\nEnter another combination.");
                var userdigitsList = new List<char>(Console.ReadLine());
                userArray = userdigitsList.ConvertAll(x => Int32.Parse(x.ToString())).ToArray();
            }
            
            return userArray;
        }

        private static bool CheckForLettersAndSpecialCharacters(string userdigits)
        {
            Regex regex = new Regex(@"^\d+$");
            return regex.IsMatch(userdigits);
        }

        private static int[] RandomGeneratedAnswer()
        {
            string randomGenAnswer = string.Empty;
            Random random = new Random();
            for (int i = 1; i < 5; i++)
            {
                randomGenAnswer = randomGenAnswer.Insert(randomGenAnswer.Length, random.Next(MINNUMBER, MAXNUMBER).ToString());
            }
            return ValidationUserDigits(randomGenAnswer);
        }

        private static List<int> GetMatchingDigits(int[] players4Digits, int[] randomGenAnswer)
        {
            int matchingCount = 0;
            List<int> countsList = new List<int>();

            for (int i = 0; i < players4Digits.Length; i++)
            {
                int x = players4Digits[i];

                for (int j = 0; j < randomGenAnswer.Length; j++)
                {
                    int y = randomGenAnswer[j];
                    if (x == y)
                    {
                        countsList.Add(matchingCount++);
                        break;
                    }
                }
            }
            return countsList;
        }

        private static string MatchingDigits(List<int> matchingCount)
        {
            var matching = matchingCount.Count;
            string response = string.Empty;

            switch (matching)
            {
                case 1:
                    response = "+";
                    break;
                case 2:
                    response = "++";
                    break;
                case 3:
                    response = "+++";
                    break;
                case 4:
                    response = "Congratulations you have won!";
                    break;
                default:
                    break;
            }
            return response;
        }

        private static string IncorrectDigits(List<int> incorrectCount)
        {
            var incorrectDigits = 4 - incorrectCount.Count;
            string response = string.Empty;

            switch (incorrectDigits)
            {
                case 1:
                    response = "-";
                    break;
                case 2:
                    response = "--";
                    break;
                case 3:
                    response = "---";
                    break;
                default:
                    break;
            }
            return response;
        }

        private static bool ContinuePlaying()
        {
            Console.WriteLine("Do you want to continue playing? (Y/N)");
            System.Threading.Thread.Sleep(2000);
            string playerOption = Console.ReadLine();
            playerOption.ToLower();
            return playerOption.Equals("y") ? true : false;
        }
        #endregion
    }
}
