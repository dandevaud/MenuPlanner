// <copyright file="IngredientSearch.razor.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.models.Search;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;

namespace MenuPlanner.Client.Controls.IngredientControls
{
    public partial class IngredientSearch
    {

        private List<Ingredient> FoundIngredients { get; set; }
        
        private int selection = -1;

        [Parameter]
        public SearchResponseModel<Ingredient> Ingredient { get; set; } = new SearchResponseModel<Ingredient>();

        [Parameter]
        public EventCallback<SearchResponseModel<Ingredient>> IngredientChanged { get; set; }

        [Parameter]
        public EventCallback Submit { get; set; }

        private string filter = "";

        [Parameter]
        public string DivClass { get; set; } = "form-group";

        [Parameter]
        public string InputClass { get; set; } = "form-control";



        private async Task GetFilteredIngredients()
        {
           if (filter.Length > 0)
            {
                FoundIngredients =
                    ((await PublicClient.Client.GetFromJsonAsync<SearchResponseModel<Ingredient>>(
                        $"api/Search/IngredientBy?filter={filter}&count=10")) ?? new SearchResponseModel<Ingredient>()).Result;
            }
            else
            {
                FoundIngredients = Array.Empty<Ingredient>().ToList();
            }

           if (selection > FoundIngredients.Count) selection = -1;

            StateHasChanged();

        }

        private async Task OnInputChanged(ChangeEventArgs e)
        {
            filter = e.Value.ToString();
           await GetFilteredIngredients();
        }

        
        private async Task KeyUp(KeyboardEventArgs e)
        {
            //https://stackoverflow.com/questions/63861068/blazor-how-can-i-trigger-the-enter-key-event-to-action-a-button-function
            //Enter will submit
            if (e.Code.Equals("ArrowUp"))
            {
                if (selection > 0)
                {
                    selection--;
                }

                if (selection == 0)
                {
                    selection = -1;
                }
            } else if (e.Code.Equals("ArrowDown"))
            {
                if (selection < 0 && selection<FoundIngredients.Count)
                {
                    selection = 0;
                }
                else if (selection + 1 < FoundIngredients.Count)
                {
                    selection++;
                }
                
            }
            else if (e.Code.Equals("Enter"))
            {
                if (FoundIngredients.Count > 0)
                {
                    if (selection < 0)
                    {
                        FoundIngredients =
                            ((await PublicClient.Client.GetFromJsonAsync<SearchResponseModel<Ingredient>>(
                                $"api/Search/IngredientBy?filter={filter}")) ?? new SearchResponseModel<Ingredient>())
                            .Result;
                        await SetIngredient(FoundIngredients);
                    }
                    else
                    {
                        await SetIngredient(new List<Ingredient>() {FoundIngredients[selection]});
                    }
                }
                else
                {
                    await Submit.InvokeAsync();
                }


            }
        }

        private String IsActive(int i)
        {
            return i == selection ? "active" : "";
        }

        private async Task SetIngredient(List<Ingredient> ing)
        {
            
            Ingredient.Result =  ing;
            Ingredient.TotalResults = ing.Count;
            FoundIngredients = new List<Ingredient>();
            filter = "";
            selection = -1;
            await IngredientChanged.InvokeAsync(Ingredient);
            StateHasChanged();
        }

    }
}
