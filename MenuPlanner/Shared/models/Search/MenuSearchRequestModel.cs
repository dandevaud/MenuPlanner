using MenuPlanner.Shared.models.enums;
using MenuPlanner.Shared.models;
namespace Shared.models.Search
{
    public class MenuSearchRequestModel
    {
       public string Name { get; set; }
       public double AverageRating { get; set; }
       public int Votes { get; set; }
        public TimeOfDay TimeOfDay { get; set; } 
        public Season Season { get; set; } 
        public MenuCategory MenuCategory { get; set; }
        public MenuIngredient Ingredient{get; set;}
    }
}