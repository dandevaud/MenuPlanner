// <copyright file="ChangeTrackingExtension.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MenuPlanner.Server.Extension.EntityFramework
{
    public static class ChangeTrackingExtension
    {
        public static void DetachUnchanged(this EntityEntry entry)
        {
            if (entry.State.Equals(EntityState.Unchanged))
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
