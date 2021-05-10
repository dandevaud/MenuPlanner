// <copyright file="AddEditDeleteMenu.razor.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MenuPlanner.Client.Controls.GeneralControls;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.models.Search;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace MenuPlanner.Client.Pages
{
    public partial class AddEditDeleteMenu
    {

        [Parameter]
        public Guid? Id { get; set; }

        private Menu menu;

        private SearchResponseModel<Ingredient> ingredients = new SearchResponseModel<Ingredient>();
        private Ingredient ingredient
        {
            get
            {
                if (ingredients == null || ingredients.Count.Equals(0))
                {
                    return null;
                }
                return ingredients.Result[0];
            }
            set => ingredient = value;
        }

        private Double Rating;

        private bool isLoading = false;

        private Modal modal { get; set; }

        protected override async Task OnInitializedAsync()
        {

            if (Id == null)
            {
                menu = new Menu();
                return;
            }

            try
            {
                menu = await Http.GetFromJsonAsync<Menu>($"api/Menus/{Id}");
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }


        private async Task UpdateMenu()
        {
            try
            {
                isLoading = true;
                SetRating();
                HttpResponseMessage response;
                if (Id == null)
                {
                    response = await Http.PostAsJsonAsync<Menu>("api/Menus", menu);
                    menu = await response.Content.ReadFromJsonAsync<Menu>();
                }
                else
                {
                    response = await Http.PutAsJsonAsync<Menu>($"api/Menus/{Id}", menu);

                }
                if (response.IsSuccessStatusCode)
                {
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(errorMessage);
                    //Implement ErrorComponent
                }
            }
            finally
            {
                isLoading = false;
            }
        }

        private void RemoveMenuIngredient(MenuIngredient menuIngredient)
        {
            menu.Ingredients.Remove(menuIngredient);
        }

        private void AddIngredientToMenu(MenuIngredient menuIngredient)
        {
            if (menu.Ingredients == null)
            {
                menu.Ingredients = new List<MenuIngredient>();
            }

            menu.Ingredients.Add(menuIngredient);

            UpdateIngredientToMenu(menuIngredient);
        }

        private void UpdateIngredientToMenu(MenuIngredient menuIngredient)
        {
            ingredient = new Ingredient();
            StateHasChanged();
        }

        private void SetRating()
        {
            if (Rating > 0)
            {
                menu.AverageRating = ((menu.AverageRating * menu.Votes) + Rating) / (menu.Votes + 1);
                menu.Votes++;
            }
        }

        private void StoreRating(int rating)
        {
            Rating = rating;
        }

        public async Task DeleteMenu()
        {
            bool confirm = true;
            if (confirm)
            {
                var response = await Http.DeleteAsync($"api/Menus/{Id}");

                if (response.IsSuccessStatusCode)
                {
                    NavigationManager.NavigateTo("/");
                }
            }
        }

        public IEnumerable<IGrouping<string, MenuIngredient>> GroupMenuIngredient()
        {
            return menu.Ingredients.GroupBy(i => i.Grouping);
        }

    }
}
