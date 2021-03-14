// <copyright file="AddIngredient.razor.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MenuPlanner.Client.Shared;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.models.Search;
using Microsoft.AspNetCore.Components;

namespace MenuPlanner.Client.Pages
{
    public partial class AddIngredient
    {
        [Parameter]
        public Guid? Id { get; set; }
        private Ingredient newIngredient = new Ingredient();
        private bool isLoading = false;
        private bool isCategoryDisabled;

        protected override async Task OnParametersSetAsync() {
            newIngredient.ParentIngredients = new List<Ingredient>();
            if (Id == null)
                {
                    
                    return;
                }
            newIngredient = await Http.GetFromJsonAsync<Ingredient>($"api/Ingredients/{Id}");
                

        }


        private async Task AddIngredientTask()
        {
            try
            {
                isLoading = true;

                await Http.PostAsJsonAsync<Ingredient>("api/Ingredients", newIngredient);
                newIngredient = new Ingredient();
                newIngredient.ParentIngredients = new List<Ingredient>();
                isCategoryDisabled = false;
            }
            finally
            {
                isLoading = false;
            }
        }
        private void StoreParentIngredient(SearchResponseModel<Ingredient> ingredients)
        {
            var ingredient = ingredients.Result[0];
            if (newIngredient.ParentIngredients.All(i => i.Id != ingredient.Id))
            {
                newIngredient.ParentIngredients.Add(ingredient);
            }
            newIngredient.Category = ingredient.Category;
            isCategoryDisabled = true;
        }

        private void RemoveParentIngredient(Ingredient ingredient)
        {
            newIngredient.ParentIngredients.Remove(ingredient);
        }
    }
}
