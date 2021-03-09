// <copyright file="IngredientSearch.razor.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MenuPlanner.Shared.models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MenuPlanner.Client.Controls.IngredientControls
{
    public partial class IngredientSearch
    {

        private List<Ingredient> FoundIngredients { get; set; }

        [Parameter]
        public Ingredient Ingredient { get; set; }

        [Parameter]
        public EventCallback<Ingredient> IngredientChanged { get; set; }

        private string filter = "";

        [Parameter]
        public string DivClass { get; set; } = "form-group";

        [Parameter]
        public string InputClass { get; set; } = "form-control";



        private async Task GetFilteredIngredients(ChangeEventArgs e)
        {
            filter = e.Value.ToString();
            if (filter.Length > 0)
            {
                FoundIngredients =
                    ((await PublicClient.Client.GetFromJsonAsync<Ingredient[]>(
                        $"api/Search/IngredientBy?filter={filter}&count=10")) ?? Array.Empty<Ingredient>()).ToList();
            }
            else
            {
                FoundIngredients = Array.Empty<Ingredient>().ToList();
            }

            StateHasChanged();

        }



        private async Task Enter(KeyboardEventArgs e)
        {
            //https://stackoverflow.com/questions/63861068/blazor-how-can-i-trigger-the-enter-key-event-to-action-a-button-function
            //Enter will submit
            if (e.Code == "Tab")
            {
                if (FoundIngredients.Count == 1)
                {
                    await SetIngredient(FoundIngredients[0]);

                }
            }
        }

        private async Task SetIngredient(Ingredient ing)
        {
            Ingredient = ing;
            FoundIngredients = new List<Ingredient>();
            filter = "";
            await IngredientChanged.InvokeAsync(Ingredient);
            StateHasChanged();
        }

    }
}
