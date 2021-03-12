// <copyright file="IngredientCategory.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System.Collections.Generic;

namespace MenuPlanner.Shared.models.Search
{
    public class SearchResponseModel<TResult>
    {
        public List<TResult> Result { get; set; } = new List<TResult>();
        public string OrderBy {get; set;}

        public int TotalResults { get; set; }
        public int Skip { get; set; }
        public int Count { get; set; }
    }
}
