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