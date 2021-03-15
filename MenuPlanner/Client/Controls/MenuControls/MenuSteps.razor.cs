// <copyright file="MenuSteps.razor.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Shared.models;
using Microsoft.AspNetCore.Components;

namespace MenuPlanner.Client.Controls.MenuControls
{
    public partial class MenuSteps
    {
       
            // used https://stackoverflow.com/questions/60416018/how-to-bind-to-element-from-collection-list-in-blazor

            [Parameter]
            public Menu Menu { get; set; }

        [Parameter]
        public List<String> Steps { get; set; }

        private string initialStepDescription = "Add your step description here";



        protected override void OnInitialized()
        {

            Steps = Menu?.Steps?.ToList() ?? new List<string>()
            {
                initialStepDescription
            };

        }

        private void OnChange(ChangeEventArgs e, int index)
        {
            Steps[index] = e.Value.ToString();
            UpdateSteps();
        }

        private void UpdateSteps()
        {
            Menu.Steps = Steps;
        }


        private void AddStep()
        {
            Steps.Add(initialStepDescription);
            StateHasChanged();
        }

        private void RemoveStep(string text)
        {
            Steps.Remove(text);
            UpdateSteps();
            StateHasChanged();
        }



    }
}

