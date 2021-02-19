using System;
using MenuPlanner.Shared.models;
using Microsoft.AspNetCore.Components;

namespace MenuPlanner.Client.Controls.MenuControls.ShowMenu
{
    public partial class IngredientDisplay
    {
        [Parameter]
        public MenuIngredient Ing { get; set; }

        [Parameter]
        public decimal Fraction { get; set; }

        private decimal GetCorrectQuantity()
        {
            var quantity = Convert.ToDecimal(Ing.Quantity.QuantityValue);
            return Decimal.Round(Decimal.Multiply(quantity, Fraction), 2);
        }
    }
}
