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
using MySqlX.XDevAPI.Common;

namespace MenuPlanner.Client.Pages
{
    public partial class Index
    {

     
        private readonly int count = 15;
        private int skip = 0;
        private SearchResponseModel<Menu> Response { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetMenus();
        }

        public async Task GetMenus()
        {
            Response =
                await PublicClient.Client.GetFromJsonAsync<SearchResponseModel<Menu>>(
                    $"api/Menus?count={count}&skip={skip}");


        }

        private void MenusChanged(SearchResponseModel<Menu> menusChanged)
        {
            skip = menusChanged.Skip;
            Response = menusChanged;
        }

        private async Task OnSkipChanged(int skip)
        {
            this.skip = skip;
            await GetMenus();
        }
    }
}
