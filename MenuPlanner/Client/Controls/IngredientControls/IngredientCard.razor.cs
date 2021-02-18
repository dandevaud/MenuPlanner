// <copyright file="IngredientCard.razor.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Shared.models;
using Microsoft.AspNetCore.Components;

namespace MenuPlanner.Client.Controls.IngredientControls
{
    public partial class IngredientCard
    {

        [Parameter]
        public Ingredient Ingredient { get; set; } = new Ingredient();

        [Parameter]
        public EventCallback<MenuIngredient> MenuIngredientChanged { get; set; }

        [Parameter]
        public EventCallback<MenuIngredient> RemoveMenuIngredient { get; set; }

        [Parameter]
        public EventCallback<MenuIngredient> UpdateMenuIngredient { get; set; }

        [Parameter]
        public string Css { get; set; } = "";

        [Parameter]
        public MenuIngredient MenuIngredient { get; set; } = new MenuIngredient()
        {
            Quantity = new Quantity()
        };

        [Parameter]
        public bool ToggleEditMode { get; set; } = false;

        private AddOrUpdate addOrUpdate = AddOrUpdate.Add;

        private enum AddOrUpdate
        {
            Add, Update
        }


        private void EditMenuIngredient()
        {
            ToggleEditMode = true;
            addOrUpdate = AddOrUpdate.Update;
            StateHasChanged();
        }

        protected override void OnParametersSet()
        {
            if (MenuIngredient.Ingredient != null) Ingredient = MenuIngredient.Ingredient;
           

        }

        private async Task AddIngredient()
        {
            MenuIngredient.Ingredient = Ingredient;
           
            if (addOrUpdate.Equals(AddOrUpdate.Add))
            {
                await MenuIngredientChanged.InvokeAsync(MenuIngredient);
            }
            else
            {
                await UpdateMenuIngredient.InvokeAsync(MenuIngredient);

            }


        }
    }
}
