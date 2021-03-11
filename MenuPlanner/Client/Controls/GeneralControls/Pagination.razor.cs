// <copyright file="Pagination.razor.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Org.BouncyCastle.Crypto.Macs;

namespace MenuPlanner.Client.Controls.GeneralControls
{
    public partial class Pagination
    {

        [Parameter] public int Count { get; set; } = 20;
        [Parameter] public EventCallback<int> CountChanged { get; set; }

        [Parameter] public int Skip { get; set; } = 0;
        [Parameter] public EventCallback<int> SkipChanged { get; set; }

        [Parameter] public int Total { get; set; }

        [Parameter] public int SurroundingFields { get; set; } = 2;

        private int internalCount = 1;

        private int last;

        protected override void OnParametersSet()
        {
            last = Convert.ToInt32(Math.Ceiling(new Decimal(Total / Count)));
        }

        private async Task OnNumberClick(int number)
        {
            internalCount = number;
            await UpdateSkip();
        }

        private async Task OnArrowClick(int number)
        {
            internalCount += number;
            await UpdateSkip();
        }

        private async Task UpdateSkip()
        {
            Skip = (internalCount - 1) * Count;
            await SkipChanged.InvokeAsync(Skip);
        }

        private String IsActive(int i)
        {
            if (i == internalCount) return "active";
            return "";
        }

        private bool GetPublished(int i)
        {
            // do not question the Math!
            var low = internalCount - 2 * SurroundingFields - 1;
            bool lowBool = SurroundingFields==1 ? (low < 0 && i <= 4) : (low < 0 && i <= (2 * SurroundingFields + 3));
            bool absolutBool = (Math.Abs(internalCount - i) < (SurroundingFields + 1));
            return absolutBool || lowBool;
        }

        private string GetDisabled(bool condition)
        {
            if (condition) return "disabled";
            return "";
        }
    }
}
