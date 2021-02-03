using System.Threading.Tasks;
using MenuPlanner.Shared.models;

namespace MenuPlanner.Server.Contracts.Logic
{
    public interface IMenuEntityUpdater
    {
        Task UpdateMenuInContext(Menu menu);
        Task DeleteMenuFromDatabase(Menu menuIn);
    }
}
