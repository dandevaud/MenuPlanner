using System.Collections.Generic;
using MenuPlanner.Shared.models.enums;
using MenuPlanner.Shared.models;
namespace Shared.models.Search
{
    public class IngredientSearchRequestModel : SearchRequestModel
    {
     
        public ICollection<Ingredient> ParentIngredients { get; set; } = new List<Ingredient>();
        public ICollection<Ingredient> ChildIngredients { get; set; } = new List<Ingredient>();
        public IngredientCategory Category { get; set; }
        public int Calories { get; set; }
        public double Price { get; set; }

                
    }
}
