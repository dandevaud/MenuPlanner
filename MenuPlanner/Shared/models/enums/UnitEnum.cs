// <copyright file="UnitEnum.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System.ComponentModel;

namespace MenuPlanner.Shared.models.enums
{
    public enum UnitEnum
    {
        //TODO: Defines Units
        [Description("Gram")]
        g = 0,
        [Description("Kilo Gram")]
        kg = 1,
        [Description("Deci Liter")]
        dl = 2,
        [Description("Liter")]
        l = 3,
        [Description("Teaspoon")]
        tsp = 4,
        [Description("Tablespoon")]
        tbsp = 5,
        [Description("Piece")]
        piece = 6
        
    }
}
