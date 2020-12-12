// <copyright file="User.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;

namespace MenuPlanner.Shared.models
{
    public class User
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
