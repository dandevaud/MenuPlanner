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
        public int Value { get; set; }

        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }

        [Parameter]
        public Dictionary<string,int> MaxValues { get; set; }

        [Parameter]
        public string FieldId { get; set; }

        [Parameter]
        public string Text { get; set; }

        private int ValueInternal
        {
            get => Value;
            set => ValueChanged.InvokeAsync(value);
        }
                
    }
}
