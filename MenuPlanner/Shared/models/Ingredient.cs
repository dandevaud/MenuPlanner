using System;
using System.Collections.Generic;
using MenuPlanner.Shared.models.enums;

namespace MenuPlanner.Shared.models
{
    public class Ingredient
    {
        public Guid Id;
        public string Name;
        public Ingredient ParentIngredient;
        public List<Ingredient> ChildIngredients;
        public IngredientCategory Category;

        public int Calories;
        public int Price;

       
    }
}