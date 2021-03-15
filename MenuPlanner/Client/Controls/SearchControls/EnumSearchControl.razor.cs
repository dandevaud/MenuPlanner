// <copyright file="EnumSearchControl.razor.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MenuPlanner.Client.Controls.SearchControls
{
    public delegate string GetActiveAttribute<T>(T option) where T : struct, Enum;
    public delegate void UpdateEnumValues<T>(T e);

    public partial class EnumSearchControl<TEnum> where TEnum : struct, Enum
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public TEnum StandardValue { get; set; }

        [Parameter]
        public GetActiveAttribute<TEnum> GetActiveAttribute { get; set; }

        [Parameter]
        public UpdateEnumValues<TEnum> UpdateEnumValues { get; set; }

        [Parameter]
        public string Class { get; set; } = "col col-3";

    }
}
