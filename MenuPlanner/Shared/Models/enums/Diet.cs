// <copyright file="Class1.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuPlanner.Shared.Models.enums
{
     [Flags]
    public enum Diet
    {

        [Description("None")]
        None = 0,
        [Description("Vegan")]
        Vegan = 1,
        [Description("Vegetarian")]
        Vegetarian = 2,
        [Description("Low-Carb")]
        LowCarb = 4,
        [Description("Gluten-Free")]
        GlutenFree = 8,
        [Description("Lactose-Free")]
        LactoseFree = 16
    }
}
