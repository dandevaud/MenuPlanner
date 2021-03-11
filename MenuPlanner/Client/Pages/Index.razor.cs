// <copyright file="Index.razor.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.models.Search;

namespace MenuPlanner.Client.Pages
{
    public partial class Index
    {

        private List<Menu> menus;

        protected override async Task OnInitializedAsync()
        {
            await GetMenus();
        }

        public async Task GetMenus()
        {
            menus = (await PublicClient.Client.GetFromJsonAsync<SearchResponseModel<Menu>>("api/Menus")).Result;
        }

        private void MenusChanged(List<Menu> menusChanged)
        {
            menus = menusChanged;
        }

        private void OnSkipChanged(int skip)
        {
            Console.WriteLine(skip);
        }
    }
}
