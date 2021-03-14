// <copyright file="IngredientList.razor.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MenuPlanner.Shared.Extension;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.models.Search;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace MenuPlanner.Client.Pages
{
    public partial class IngredientList
    {
        private SearchResponseModel<Ingredient> ingredients;
        private int skip = 0;
        private int count = 15;


        private async Task OnIngredientsChange(SearchResponseModel<Ingredient> ingredient)
        {
            if (ingredient.Result.Count == 1)
            {
                NavigationManager.NavigateTo($"addingredient/{ingredient.Result[0].Id}");
            }
            else
            {
                var request = ingredient.Request;
                request.Count = count;
                request.Skip = 0;
                skip = 0;
               await LoadIngredients(request);
                StateHasChanged();
            }
        }

        protected override async Task OnInitializedAsync()
        {
           await LoadIngredients(new SearchRequestModel() { Count = count, Skip = skip });
        }


        private async Task DeleteIngredient(Guid id)
        {
            var response = await Http.DeleteAsync($"api/Ingredients/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();

                await LoadIngredients(new SearchRequestModel(){Count = count, Skip = skip});

            }
            else
            {
                //todo
            }
        }

        private async Task LoadIngredients(SearchRequestModel searchRequest)
        {
            try
            {
                ingredients = await Http.GetFromJsonAsync<SearchResponseModel<Ingredient>>($"api/Ingredients?{searchRequest.ToQueryString()}");
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }



        private async Task OnSkipChanged(int skip)
        {
            this.skip = skip;
            ingredients.Request.Skip = skip;
            await LoadIngredients(ingredients.Request);
        }
        

      
    }
}
