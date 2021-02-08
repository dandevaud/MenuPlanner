using System;
using System.Threading.Tasks;
using MenuPlanner.Shared.models;

namespace MenuPlanner.Server.Contracts.Logic
{
    public interface IIngredientEntityUpdater
    {
        Task CheckIfIngredientExistsAndUpdateOrAdd(Ingredient ingredient);
        Task<bool> DeleteIngredient(Guid id);
    }
}
