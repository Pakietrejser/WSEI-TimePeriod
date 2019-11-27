using System;
using TimeTimePeriod;

namespace TimeDemo.Properties
{
    public class ProjectDemo
    {
        private Time time;
        
        public static void Main(string[] args)
        {
            new ProjectDemo().Init();
        }

        private void Init()
        {
            bool isPlaying = true;
            time = GetCurrentTime();
            PrintStartScreen();
            
            do
            {
                PrintCurrentTime();
                PrintOptionsScreen();
                int userOption = GetUserOption();
                
                if (userOption == 1)
                {
                    ChangeTime(true);
                }
                else if (userOption == 2)
                {
                    ChangeTime(false);
                }
                else if (userOption == 3)
                {
                    ResetTime();
                }
                else if (userOption == 4)
                {
                    isPlaying = false;
                }
            } while (isPlaying);
        }

        

        private void ChangeTime(bool add)
        {
            Console.WriteLine("How long do you want to travel? Use a proper TimePeriod format.");
            do
            {
                string userInput = Console.ReadLine();
                try
                {
                    TimePeriod timePeriod = new TimePeriod(userInput);
                    time = add ? time.Plus(timePeriod) : time.Minus(timePeriod);
                    Console.WriteLine("You've travelled for {0} seconds...",timePeriod.Seconds);
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Use a proper TimePeriod format! (hh:mm:ss, mm:ss, or ss)");
                }
            } while (true);
        }

        private int GetUserOption()
        {
            int userOption = 0;
            do
            {
                string userInput = Console.ReadLine();
                try
                {
                    userOption = Convert.ToInt32(userInput);
                }
                catch (Exception)
                {
                    Console.WriteLine("Value must be a number!");
                }
            } while (userOption < 1 || userOption > 4);
            return userOption;
        }
        
        private Time GetCurrentTime()
        {
            DateTime now = DateTime.Now;
            return new Time((byte)now.Hour, (byte)now.Minute, (byte)now.Second);
        }
        
        private void ResetTime()
        {
            time = GetCurrentTime();
        }

        private void PrintCurrentTime()
        {
            Console.WriteLine("\n--------------------------------------");
            Console.WriteLine("Current time: {0} ",time.ToString());
            Console.WriteLine();
        }

        private void PrintStartScreen()
        {
            Console.WriteLine("WELCOME TO TIME WIZARD!");
            Console.WriteLine("In this application you can change time!");
        }

        private void PrintOptionsScreen()
        {
            Console.WriteLine("If you wish to change time, press...");
            Console.WriteLine("1. Travel to the future");
            Console.WriteLine("2. Go back to the past");
            Console.WriteLine("3. Reset to the boring current time");
            Console.WriteLine("4. Exit the application");
        }
    }
}