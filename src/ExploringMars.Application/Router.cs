using System;
using System.Threading;
using ExploringMars.Application.Exceptions;

namespace ExploringMars.Application
{
    public class Router : IRouter
    {
        private readonly AutoResetEvent WaitHandle;

        public Router()
        {
            WaitHandle =  new AutoResetEvent(false);
        }
        
        public void Run(bool isFirstRun = true)
        {
            try
            {
                DisplayWelcomeMessage(isFirstRun);

                Controller.GetProbesLandingPositions();

                WaitCancelKeyPress();
            }
            catch (InvalidInputException)
            {
                Resume();
            }
        }

        private static void DisplayWelcomeMessage(bool isFirstRun)
        {
            if (isFirstRun)
            {
                Console.WriteLine("Hello there!\n" +
                                  "🚀🚀🚀🚀Welcome to the ExploringMars application. I am going to help you find out where your Probes are going to land at the Mars Plateau\n" +
                                  "If you want to exit the application, just press CTRL+C or CTRL+Break\n" +
                                  "-----------------\n" +
                                  "Now, let's start!\n" +
                                  "-----------------\n");
            }
        }

        private static void WaitCancelKeyPress()
        {
            Console.CancelKeyPress += (o, e) =>
            {
                Console.WriteLine("Shutting down...");
                
                WaitHandle.Set();
            };
            
            WaitHandle.WaitOne();
        }

        private static void DisplayInvalidInputMessage()
        {
            Console.WriteLine("\nOoops! Looks like you've entered an invalid input.\n" + 
                              "Let's try again from where we've left!\n" +
                              "-----------------\n");
        }

        private static void Resume()
        {
            DisplayInvalidInputMessage();
            Run(false);
        }
    }
}