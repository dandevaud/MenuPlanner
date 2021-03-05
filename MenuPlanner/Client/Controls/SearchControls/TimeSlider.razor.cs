// <copyright file="TimeSlider.razor.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Json;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.models.enums;
using MenuPlanner.Shared.models.Search;
using Microsoft.AspNetCore.Components;

namespace MenuPlanner.Client.Controls.SearchControls
{
    public partial class TimeSlider
    {
        [Parameter]
        public Int32 Value { get; set; }

        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }

        [Parameter]
        public Dictionary<string,int> MaxValues { get; set; }

        [Parameter]
        public string FieldId { get; set; }

        [Parameter]
        public string Text { get; set; }

        private async Task OnChange(ChangeEventArgs e)
        {
            var intValue = Int32.Parse(e.ToString());
            await ValueChanged.InvokeAsync(intValue);
        }

    }
}
