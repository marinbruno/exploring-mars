using System.Threading.Tasks;

namespace ExploringMars.Application
{
    public interface IRouter
    {
        Task Run(bool isFirstRun = true);
    }
}