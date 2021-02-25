// <copyright file="IngredientCategory.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System.Collections.Generic;
using MenuPlanner.Shared.models.enums;

namespace MenuPlanner.Shared.models.Search
{
    public class MenuSearchRequestModel : SearchRequestModel
    {
        public double AverageRating { get; set; }
        public int Votes { get; set; }
        public TimeOfDay TimeOfDay { get; set; }
        public Season Season { get; set; }
        public MenuCategory MenuCategory { get; set; }
        public Diet Diet { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
