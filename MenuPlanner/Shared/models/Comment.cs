// <copyright file="Comment.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;

namespace MenuPlanner.Shared.models
{
    public class Comment
    {
        public Guid Id { get; }
        public Menu Menu { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
    }
}
