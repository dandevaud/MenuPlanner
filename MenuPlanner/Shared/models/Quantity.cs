// <copyright file="Quantity.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using MenuPlanner.Shared.models.enums;

namespace MenuPlanner.Shared.models
{
    public class Quantity
    {
        public UnitEnum unit { get; set; }
        public double QuantityValue { get; set; }
    }
}
