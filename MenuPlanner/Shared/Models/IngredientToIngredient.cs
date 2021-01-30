using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuPlanner.Shared.models
{
    public class IngredientToIngredient
    {
       
        public Guid ParentId { get; set; }
        public Ingredient Parent { get; set; }

        public Guid ChildId { get; set; }
        public Ingredient Child { get; set; }
    }
}
