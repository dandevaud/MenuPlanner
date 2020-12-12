// <copyright file="Season.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;

namespace MenuPlanner.Shared.models.enums
{
    [Flags]
    public enum Season : short
    {
        Unknown = 0,
        Spring = 1,
        Summer = 2,
        Fall = 4,
        Winter = 8
    }
}
