using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Server.Data;
using MenuPlanner.Shared.models;
using Microsoft.EntityFrameworkCore;

namespace MenuPlanner.Server.Logic
{
    /// <summary>Class used to update Ingredient entities in Database</summary>
    public class IngredientEntityUpdater
    {
        private readonly MenuPlannerContext _context;

        public IngredientEntityUpdater(MenuPlannerContext dbContext)
        {
            _context = dbContext;
        }

        /// <summary>
        ///   <para>
        /// Checks if ingredient exists and update or adds it accordingly.
        /// </para>
        ///   <para>Will check by name or ID</para>
        /// </summary>
        /// <param name="ingredient">The ingredient.</param>
        public async Task CheckIfIngredientExistsAndUpdateOrAdd(Ingredient ingredient)
        {
            if (await _context.Ingredients.AnyAsync(x => x.Name.Equals(ingredient.Name)))
            {
                var existing = await _context.Ingredients.FirstAsync(x => x.Name.Equals(ingredient.Name)) ?? await _context.Ingredients.FindAsync(ingredient.IngredientId);
                await HandleExitstingEntity(ingredient, existing);
            }
            else if (_context.Ingredients.FindAsync(ingredient.IngredientId).Result != null)
            {
                var existing = await _context.Ingredients.FindAsync(ingredient.IngredientId);

                await HandleExitstingEntity(ingredient, existing);
            }
            else
            {
                await UpdateEachParentIngredient(ingredient, ingredient);
                _context.Ingredients.Add(ingredient);

            }

            await _context.SaveChangesAsync();
        }

        private async Task HandleExitstingEntity(Ingredient ingredient, Ingredient existing)
        {
            await _context.Entry(existing).Collection(i => i.ChildIngredients).LoadAsync();
            await _context.Entry(existing).Collection(i => i.ParentIngredients).LoadAsync();
            ingredient.IngredientId = existing.IngredientId;
            _context.Ingredients.Update(existing)?.CurrentValues?.SetValues(ingredient);
            await UpdateEachParentIngredient(existing, ingredient);
        }

        private async Task UpdateEachParentIngredient(Ingredient ingredient, Ingredient provided)
        {
            var updatedList = ingredient.ParentIngredients;
            if (ingredient != provided)
            {
                //By design it is checked if it is the same object --> it is if the ingredient is new --> see else statment
                await _context.Entry(ingredient)?.Collection(i => i.ChildIngredients).LoadAsync();
                await _context.Entry(ingredient)?.Collection(i => i.ParentIngredients).LoadAsync();

                var updatedAddedParentIngredients = provided.ParentIngredients.Select(ppi => ppi.IngredientId).ToList()
                    .Except(ingredient.ParentIngredients.Select(ipi => ipi.IngredientId).ToList()).ToList();

                //remove Child Ingredients from removed Parents
                ingredient.ParentIngredients.Select(pi => pi.IngredientId).ToList().Except(provided.ParentIngredients.Select(ppi => ppi.IngredientId).ToList()).ToList().ForEach(async i =>
                {
                    var entry = await _context.Ingredients.FindAsync(i);
                    var entity = _context.Ingredients.Update(entry);
                    await entity.Collection(pi => pi.ChildIngredients).LoadAsync();
                    entity.Entity.ChildIngredients.Remove(ingredient);
                });
                updatedAddedParentIngredients.ForEach(async uapi =>
                {
                    updatedList = await AddIngredientToChildIngredientOfParent(ingredient, uapi, updatedList);
                });


            }
            else
            {
                updatedList = new List<Ingredient>();
                ingredient.ParentIngredients.Select(i => i.IngredientId).ToList().ForEach(async uapi =>
                {
                    updatedList = await AddIngredientToChildIngredientOfParent(ingredient, uapi, updatedList);
                });

            }

            ingredient.ParentIngredients = updatedList;


        }

        private async Task<ICollection<Ingredient>> AddIngredientToChildIngredientOfParent(Ingredient ingredient, Guid parentIngGuid, ICollection<Ingredient> updatedList)
        {
            var ingredientInDB = await _context.FindAsync<Ingredient>(parentIngGuid);
            var entity = _context.Entry(ingredientInDB);
            await entity.Collection(i => i.ChildIngredients).LoadAsync();
            var contextIngredientParent = entity.Entity;
            contextIngredientParent.ChildIngredients.Add(ingredient);
            updatedList.Add(contextIngredientParent);
            return updatedList;
        }
    }
}
