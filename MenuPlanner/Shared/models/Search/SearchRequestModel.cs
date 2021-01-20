// <copyright file="IngredientCategory.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

namespace MenuPlanner.Shared.models.Search
{
    public abstract class SearchRequestModel
    {
        public string Filter {get; set;}
        public string Name { get; set; }
        public bool OrderBy {get; set;}
    }
}
