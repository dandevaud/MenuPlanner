// <copyright file="Image.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;

namespace MenuPlanner.Shared.models
{
    public class Image
    {
        public Guid Id { get; }
        public string AlternativeName { get; set; }
        public Uri Path { get; set; }
        public byte[] ImageBytes { get; set; }
    }
}
