using MenuPlanner.Shared.models;
using Microsoft.AspNetCore.Components;

namespace MenuPlanner.Client.Controls.MenuControls.ShowMenu
{
    public partial class CommentDisplay
    {
        [Parameter]
        public Comment Com { get; set; }
    }
}
