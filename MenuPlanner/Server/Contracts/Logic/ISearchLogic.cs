﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.models.Search;

namespace MenuPlanner.Server.Contracts.Logic
{
    public interface ISearchLogic
    {
       Task<SearchResponseModel<Menu>> GetAllMenus(SearchRequestModel searchRequest);
       Task<SearchResponseModel<Menu>> SearchMenus(MenuSearchRequestModel searchRequest);
       Task<SearchResponseModel<Ingredient>> SearchIngredients(IngredientSearchRequestModel searchRequest);
       Task<List<Ingredient>> GetSubIngredients(Ingredient ing);
        Task<SearchResponseModel<Ingredient>> GetAllIngredients(SearchRequestModel searchRequest);
        Task<Dictionary<string, int>> GetMaxTimes();
    }
}
