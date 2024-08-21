// <copyright file="IngredientEntityUpdater.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Server.Contracts.Logic;
using MenuPlanner.Server.Data;
using MenuPlanner.Shared.models;
using Microsoft.EntityFrameworkCore;

namespace MenuPlanner.Server.Logic.EntityUpdater
{
    /// <summary>Class used to update Ingredient entities in Database</summary>
    public class IngredientEntityUpdater : EntityUpdater, IIngredientEntityUpdater
    {
        private readonly MenuPlannerContext _context;


        public IngredientEntityUpdater(MenuPlannerContext dbContext) : base(dbContext)
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
                var existing = await _context.Ingredients.FirstAsync(x => x.Name.Equals(ingredient.Name)) ?? await _context.Ingredients.FindAsync(ingredient.Id);
                await HandleExitstingEntity(ingredient, existing);
            }
            else if (_context.Ingredients.FindAsync(ingredient.Id).Result != null)
            {
                var existing = await _context.Ingredients.FindAsync(ingredient.Id);

                await HandleExitstingEntity(ingredient, existing);
            }
            else
            {
                await UpdateEachParentIngredient(ingredient, ingredient);
                _context.Ingredients.Add(ingredient);
            }

            SaveChanges();
        }

        public async Task<bool> DeleteIngredient(Guid id)
        {
            return await DeleteEntity<Ingredient>(id);
        }


        private async Task HandleExitstingEntity(Ingredient ingredient, Ingredient existing)
        {
            await _context.Entry(existing).Collection(i => i.ChildIngredients).LoadAsync();
            await _context.Entry(existing).Collection(i => i.ParentIngredients).LoadAsync();
            ingredient.Id = existing.Id;
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

                var updatedAddedParentIngredients = provided.ParentIngredients.Select(ppi => ppi.Id).ToList()
                    .Except(ingredient.ParentIngredients.Select(ipi => ipi.Id).ToList()).ToList();

                //remove Child Ingredients from removed Parents
                var toRemove = ingredient.ParentIngredients.Select(pi => pi.Id).ToList().Except(provided.ParentIngredients.Select(ppi => ppi.Id).ToList()).ToList();
                foreach (Guid i in toRemove)
                {
                    var entry = await _context.Ingredients.FindAsync(i);
                    var entity = _context.Ingredients.Update(entry);
                    await entity.Collection(pi => pi.ChildIngredients).LoadAsync();
                    entity.Entity.ChildIngredients.Remove(ingredient);
                };
                foreach (Guid uapi in updatedAddedParentIngredients)
                {
                    updatedList = await AddIngredientToChildIngredientOfParent(ingredient, uapi, updatedList);
                }
            }
            else
            {
                updatedList = new List<Ingredient>();
                var list = ingredient.ParentIngredients.Select(i => i.Id).ToList();
                foreach (Guid uapi in list)
                {
                    updatedList = await AddIngredientToChildIngredientOfParent(ingredient, uapi, updatedList);
                };
            }

            ingredient.ParentIngredients = updatedList;
        }

        private async Task<ICollection<Ingredient>> AddIngredientToChildIngredientOfParent(Ingredient ingredient, Guid parentIngGuid, ICollection<Ingredient> updatedList)
        {
            var ingredientInDB = await _context.FindAsync<Ingredient>(parentIngGuid);
            var entity = _context.Entry(ingredientInDB);
            await entity.Collection(i => i.ChildIngredients).LoadAsync();
            entity.Entity.ChildIngredients.Add(ingredient);
            entity.State = EntityState.Modified;
            updatedList.Add(entity.Entity);
            return updatedList;
        }
    }
}
