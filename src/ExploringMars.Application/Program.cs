using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ExploringMars.Application
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRouter, Router>()
                .AddSingleton<IConsoleReader, ConsoleReader>()
                .BuildServiceProvider();

            var router = serviceProvider.GetService<IRouter>();
            
            await router.Run();
        }
    }
}
