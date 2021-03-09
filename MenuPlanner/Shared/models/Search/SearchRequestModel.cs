// <copyright file="IngredientCategory.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;

namespace MenuPlanner.Shared.models.Search
{
    public abstract class SearchRequestModel
    {

        public Guid Id { get; set; }
        public string Filter {get; set;}
        public string Name { get; set; }
        public bool OrderBy {get; set;}
        public int Skip { get; set; }
        public int Count { get; set; }
    }
}
