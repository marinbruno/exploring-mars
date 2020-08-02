using System;
using System.Threading;
using System.Threading.Tasks;
using ExploringMars.Application.Exceptions;

namespace ExploringMars.Application
{
    public class Router : IRouter
    {
        private readonly AutoResetEvent _waitHandle;

        private readonly Controller _controller;

        public Router(IConsoleReader consoleReader)
        {
            _waitHandle =  new AutoResetEvent(false);
            _controller = new Controller(consoleReader);
        }
        
        public async Task Run(bool isFirstRun = true)
        {
            try
            {
                DisplayWelcomeMessage(isFirstRun);

                await _controller.GetProbesLandingPositions();

                WaitCancelKeyPress();
            }
            catch (InvalidInputException)
            {
                await Resume();
            }
        }

        private static void DisplayWelcomeMessage(bool isFirstRun)
        {
            if (isFirstRun)
            {
                Console.WriteLine("Hello there!\n" +
                                  "Welcome to the ExploringMars application. I am going to help you find out where your Probes are going to land at the Mars Plateau\n" +
                                  "If you want to exit the application, just press CTRL+C or CTRL+Break\n" +
                                  "-----------------\n" +
                                  "Now, let's start! 🚀\n" +
                                  "-----------------\n");
            }
        }

        private void WaitCancelKeyPress()
        {
            Console.CancelKeyPress += (o, e) =>
            {
                Console.WriteLine("Shutting down...");
                
                _waitHandle.Set();
            };
            
            _waitHandle.WaitOne();
        }

        private static void DisplayInvalidInputMessage()
        {
            Console.WriteLine("\nOoops! Looks like you've entered an invalid input.\n" + 
                              "Let's try again from where we've left!\n" +
                              "-----------------\n");
        }

        private async Task Resume()
        {
            DisplayInvalidInputMessage();
            await Run(false);
        }
    }
}