// <copyright file="EnumControls.razor.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Internal;

namespace MenuPlanner.Client.Controls.MenuControls.EnumControls
{
    public partial class EnumControls<TEnum> where TEnum : struct, Enum
    {
        [Parameter] public TEnum ProvidedEnum { get; set; }

        [Parameter]
        public bool isFlag { get; set; } = true;
        [Parameter] public EventCallback<TEnum> UpdateEnum { get; set; }
        [Parameter] public EventCallback<TEnum> RemoveEnum { get; set; }


#pragma warning disable EF1001 // Internal EF Core API usage.
        public bool IsHidden(TEnum timeOfDay) => typeof(TEnum).IsDefaultValue(timeOfDay);
#pragma warning restore EF1001 // Internal EF Core API usage.

        public bool IsChecked(TEnum timeOfDay)
        {

            var ret = false;
            if (isFlag)
            {
                ret = ProvidedEnum.HasFlag(timeOfDay);
            }
            else
            {
                ret = ProvidedEnum.Equals(timeOfDay);
            }
            ret |= IsHidden(timeOfDay);

            return ret;
        }

        public async Task UpdateTimeOfDay(TEnum timeOfDay)
        {
            if (IsChecked(timeOfDay))
            {
                await RemoveEnum.InvokeAsync(timeOfDay);
            }
            else
            {
                await UpdateEnum.InvokeAsync(timeOfDay);
            }
        }

    }
}
