// <copyright file="Quantity.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using MenuPlanner.Shared.models.enums;

namespace MenuPlanner.Shared.models
{
   [Serializable]
   public class Quantity
    {
        public UnitEnum Unit { get; set; }
        public double QuantityValue { get; set; }
    }
}
