using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace MenuPlanner.Client.Controls.MenuControls.ShowMenu
{
    public partial class PortionHandler
    {
        [Parameter]
        public int OriginalPortion { get; set; }

        [Parameter]
        public string PortionDescription { get; set; }

        [Parameter]
        public decimal Fraction { get; set; }

        [Parameter]
        public EventCallback<decimal> FractionChanged { get; set; }

        private int portion;

        private bool isDisabled = false;

        protected override void OnInitialized()
        {
            portion = OriginalPortion;
        }

        private async Task ChangePortion(int i)
        {
            portion += i;
            CheckDisableStatus();
            await UpdateFraction();

        }

        private async Task UpdateFraction()
        {
            Fraction = Decimal.Divide(portion, OriginalPortion);
            await FractionChanged.InvokeAsync(Fraction);
            StateHasChanged();
        }

        private void CheckDisableStatus()
        {
            if (portion == 1)
            {
                isDisabled = true;
            }
            else
            {
                isDisabled = false;
            }
        }

    }
}
