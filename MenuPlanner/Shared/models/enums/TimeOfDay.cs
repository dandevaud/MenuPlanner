// <copyright file="TimeOfDay.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;

namespace MenuPlanner.Shared.models.enums
{

    [Flags]
    public enum TimeOfDay : short
    {
        Unknown = 0,
        Breakfast = 1,
        Lunch = 2,
        Dinner = 4

    }
}
