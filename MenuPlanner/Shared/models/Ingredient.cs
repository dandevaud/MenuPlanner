// <copyright file="Ingredient.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using MenuPlanner.Shared.models.enums;

namespace MenuPlanner.Shared.models
{
    public class Ingredient
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public Ingredient ParentIngredient { get; set; }
        public List<Ingredient> ChildIngredients { get; set; }
        public IngredientCategory Category { get; set; }

        public int Calories { get; set; }
        public int Price { get; set; }


    }
}
