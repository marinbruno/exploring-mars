using Microsoft.Extensions.DependencyInjection;

namespace ExploringMars.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRouter, Router>()
                .AddSingleton<IConsoleReader, ConsoleReader>()
                .BuildServiceProvider();

            var router = serviceProvider.GetService<IRouter>();
            
            router.Run();
        }
    }
}
