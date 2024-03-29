﻿// <copyright file="ManuSearch.razor.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.models.enums;
using MenuPlanner.Shared.models.Search;
using Microsoft.AspNetCore.Components;

namespace MenuPlanner.Client.Controls.SearchControls
{
    public partial class MenuSearch
    {
        [Parameter]
        public SearchResponseModel<Menu> Results { get; set; }

        [Parameter]
        public EventCallback<SearchResponseModel<Menu>> ResultsChanged { get; set; }

        public delegate R HandleList<T, R>(T value);

        private MenuSearchRequestModel searchModel;
        private SelectedEnums selectedEnums = new SelectedEnums();

        private SearchResponseModel<Ingredient> ing = new SearchResponseModel<Ingredient>();

        private string searchString = String.Empty;

        private Boolean isAdvanced = false;

        private Dictionary<string, int> maxValues = new Dictionary<string, int>();

        private bool isAnyMatch = true;

        private class SelectedEnums
        {
            public List<TimeOfDay> TimeOfDays { get; set; } = new List<TimeOfDay>();
            public List<MenuCategory> MenuCategories { get; set; } = new List<MenuCategory>();
            public List<Season> Seasons { get; set; } = new List<Season>();
            public List<Diet> Diets { get; set; } = new List<Diet>();

            public void UpdateValues<T>(T provided) where T : struct, Enum
            {

                CheckTypeAndHandleList(provided, true, (TimeOfDay t) =>
                {
                    return RemoveOrAddToList(TimeOfDays, t);
                }, (value =>
                {
                    return RemoveOrAddToList(Seasons, value);
                }), (value =>
                {
                    return RemoveOrAddToList(MenuCategories, value);
                }), (value =>
                {
                    return RemoveOrAddToList(Diets, value);
                })
                );
            }


            public bool IsSelected<T>(T provided) where T : struct, Enum
            {
                return CheckTypeAndHandleList(provided, false, new HandleList<TimeOfDay, bool>((TimeOfDay t) => { return TimeOfDays.Contains(t); }), new HandleList<Season, bool>((Season s) => { return Seasons.Contains(s); }), new HandleList<MenuCategory, bool>((MenuCategory m) => { return MenuCategories.Contains(m); }), new HandleList<Diet, bool>((Diet m) => { return Diets.Contains(m); }));
            }

            private R CheckTypeAndHandleList<T, R>(T provided, R defaultReturn, HandleList<TimeOfDay, R> handleTimeOfDay, HandleList<Season, R> handleSeason, HandleList<MenuCategory, R> handleMenuCategory, HandleList<Diet, R> handleDiet) where T : struct, Enum where R : struct
            {
                if (provided is TimeOfDay day)
                {
                    return handleTimeOfDay(day);
                }
                else if (provided is MenuCategory category)
                {
                    return handleMenuCategory(category);
                }
                else if (provided is Season season)
                {
                    return handleSeason(season);
                } else if (provided is Diet diet)
                {
                    return handleDiet(diet);
                }
                return defaultReturn;
            }

            private bool RemoveOrAddToList<T>(List<T> list, T element) where T : struct, Enum
            {
                if (list.Contains(element)) list.Remove(element);
                else list.Add(element);
                return true;
            }

        }

        private void RemoveIngredient(Ingredient ingr)
        {
            searchModel.Ingredients.Remove(ingr);
            StateHasChanged();
        }

        private void AddToIngredientList(SearchResponseModel<Ingredient> ingr)
        {
            ingr.Result.ForEach(i => { 
                    if (!searchModel.Ingredients.Contains(i)) searchModel.Ingredients.Add(i);
            }
                );
        
            StateHasChanged();
        }


        protected override async Task OnInitializedAsync()
        {
            searchModel = new MenuSearchRequestModel();
            maxValues = await PublicClient.Client.GetFromJsonAsync<Dictionary<string, int>>("api/Menus/MaxTimes");
        }
       

        private string GetActiveAttribute<T>(T option) where T : struct, Enum
        {
            if (selectedEnums.IsSelected(option)) return "active";
            return "";
        }

        private void ChangeAdvanced()
        {
            isAdvanced = !isAdvanced;
        }

        private async Task Reset()
        {
            isAdvanced = false;
            var list = PublicClient.Client.GetFromJsonAsync<SearchResponseModel<Menu>>("api/Menus");
            var maxValuesChanged = PublicClient.Client.GetFromJsonAsync<Dictionary<string, int>>("api/Menus/MaxTimes");
            Results = await list;
            var resultChange = ResultsChanged.InvokeAsync(Results);            
            selectedEnums.MenuCategories = new List<MenuCategory>();
            selectedEnums.Seasons = new List<Season>();
            selectedEnums.TimeOfDays = new List<TimeOfDay>();
            searchModel = new MenuSearchRequestModel();
            maxValues = await maxValuesChanged;
            
            await resultChange;
        }

        private void UpdateList<T>(ChangeEventArgs e) where T : struct, Enum
        {
            T selected;
            Enum.TryParse<T>(e.Value.ToString(), out selected);
            selectedEnums.UpdateValues<T>(selected);

        }

        private bool EnumHasFlag<T>(T searchEnum, T toCheck) where T : struct, Enum
        {
            return searchEnum.HasFlag(toCheck);
        }

        private async Task GetMenus()
        {
            isAdvanced = false;
            selectedEnums.MenuCategories.ForEach(e => searchModel.MenuCategory |= e);
            selectedEnums.Seasons.ForEach(e => searchModel.Season |= e);
            selectedEnums.TimeOfDays.ForEach(e => searchModel.TimeOfDay |= e);
            selectedEnums.Diets.ForEach(e => searchModel.Diet |= e);

            if (isAnyMatch)
            {
                searchModel.Filter = searchString;
            }
            else
            {
                searchModel.Name = searchString;
            }

            searchModel.Skip = 0;
            searchModel.Count = Results.Count;
            var response = await PublicClient.Client.PostAsJsonAsync<MenuSearchRequestModel>("api/Search/MenuBy", searchModel);
            Results = await response.Content.ReadFromJsonAsync<SearchResponseModel<Menu>>();
            await ResultsChanged.InvokeAsync(Results);
        }
    }
}
