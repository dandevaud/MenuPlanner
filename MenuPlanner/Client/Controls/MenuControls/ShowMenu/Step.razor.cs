using Microsoft.AspNetCore.Components;

namespace MenuPlanner.Client.Controls.MenuControls.ShowMenu
{
    public partial class Step
    {

        [Parameter]
        public int Counter { get; set; }

        [Parameter]
        public string StepString { get; set; }

    }
}
