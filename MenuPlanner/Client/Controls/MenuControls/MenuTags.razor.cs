// <copyright file="MenuTags.razor.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Shared.models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MenuPlanner.Client.Controls.MenuControls
{
    public partial class MenuTags
    {
        [Parameter]
        public ICollection<Tag> Tags { get; set; }

        [Parameter]
        public EventCallback<ICollection<Tag>> TagsChanged { get; set; }

        [Parameter] public bool EditMode { get; set; } = false;

        private Tag tag = new Tag
        {
            Name=""
        };

        public async Task RemoveTag(Tag tag)
        {
            Tags.Remove(tag);
            await TagsChanged.InvokeAsync(Tags);
        }

        public async Task AddTag(MouseEventArgs e)
        {
            Tags.Add(new Tag(){
                Name=tag.Name
            }
            );
            tag.Name = "";
            await TagsChanged.InvokeAsync(Tags);

        }
    }
}
