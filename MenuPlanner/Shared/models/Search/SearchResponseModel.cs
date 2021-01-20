// <copyright file="IngredientCategory.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System.Collections.Generic;

namespace Shared.models.Search
{
    public class SearchResponseModel<TResult>
    {
        public List<TResult> Result {get; set;}
        public string OrderBy {get; set;}
    }
}
