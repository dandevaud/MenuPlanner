using MenuPlanner.Shared.models;

namespace MenuPlanner.Shared.Extension
{
    public static class ModelExtension
    {
        public static bool Equals(this MenuIngredient menuIngredient, MenuIngredient toCompare)
        {
            return menuIngredient.Id.Equals(toCompare.Id);
        }

        public static bool ContentEquals(this MenuIngredient menuIngredient, MenuIngredient toCompare)
        {
            var description = menuIngredient.Description?.Equals(toCompare.Description) ?? menuIngredient.Description == toCompare.Description;
            var grouping = menuIngredient.Grouping?.Equals(toCompare.Grouping) ?? menuIngredient.Grouping == toCompare.Grouping;
            var quantity = menuIngredient.Quantity?.ContentEquals(toCompare.Quantity) ?? menuIngredient.Quantity == toCompare.Quantity;

            return description && grouping && quantity;

        }

        public static bool ContentEquals(this Quantity quantity, Quantity toCompare)
        {
            return quantity.QuantityValue.Equals(toCompare.QuantityValue) &&
                   quantity.Unit.Equals(toCompare.Unit);
        }

        public static bool ContentEquals(this Ingredient ingredient, Ingredient toCompare)
        {
            return ingredient.Id.Equals(toCompare.Id) ||
                   ingredient.Name.Equals(toCompare.Name);
        }
    }
}
