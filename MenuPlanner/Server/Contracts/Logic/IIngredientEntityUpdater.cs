using System.Threading.Tasks;
using MenuPlanner.Shared.models;

namespace MenuPlanner.Server.Contracts.Logic
{
    public interface IIngredientEntityUpdater
    {
        Task CheckIfIngredientExistsAndUpdateOrAdd(Ingredient ingredient);

    }
}
