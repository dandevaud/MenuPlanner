// <copyright file="ShowMenu.razor.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MenuPlanner.Shared.models;
using Microsoft.AspNetCore.Components;

namespace MenuPlanner.Client.Pages
{
    public partial class ShowMenu
    {

        [Parameter]
        public Guid? Id { get; set; }

        private Menu menu;
        private int counter = 0;
        private decimal Fraction { get; set; }

        protected override async Task OnInitializedAsync()
        {

            if (Id == null)
            {
                menu = new Menu();
                return;
            }
            Fraction = 1;

            menu = await PublicClient.Client.GetFromJsonAsync<Menu>($"api/Menus/{Id}");
        }

        private string resetCounter()
        {
            counter = 0;
            return null;
        }


    }
}
