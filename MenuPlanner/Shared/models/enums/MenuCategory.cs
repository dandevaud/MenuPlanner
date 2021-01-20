// <copyright file="MenuCategory.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;

namespace MenuPlanner.Shared.models.enums
{
    [Flags]
    public enum MenuCategory
    {
        Unknown,
        Apero,
        Starters,
        Main,
        Dessert,
        Snacks,
        Drinks
    }
}
