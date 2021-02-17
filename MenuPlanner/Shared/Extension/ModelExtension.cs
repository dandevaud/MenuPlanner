using MenuPlanner.Shared.models;

namespace MenuPlanner.Shared.Extension
{
    public static class ModelExtension
    {
        public static bool Equals(this MenuIngredient menuIngredient, MenuIngredient toCompare)
        {
            return menuIngredient.Description.Equals(toCompare.Description) &&
                   menuIngredient.Grouping.Equals(toCompare.Grouping) &&
                   menuIngredient.Ingredient.Equals(toCompare.Ingredient) &&
                   menuIngredient.Quantity.Equals(toCompare.Quantity);
        }

        public static bool Equals(this Quantity quantity, Quantity toCompare)
        {
            return quantity.QuantityValue.Equals(toCompare.QuantityValue) ||
                   quantity.Unit.Equals(toCompare.Unit);
        }

        public static bool Equals(this Ingredient ingredient, Ingredient toCompare)
        {
            return ingredient.Id.Equals(toCompare.Id) ||
                   ingredient.Name.Equals(toCompare.Name);
        }
    }
}
