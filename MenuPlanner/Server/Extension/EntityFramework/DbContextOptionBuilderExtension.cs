// <copyright file="DbContextOptionBuilderExtension.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MenuPlanner.Server.Extension.EntityFramework
{
    public static class DbContextOptionBuilderExtension
    {
        public static DbContextOptionsBuilder EnableSensitiveDataLogging(this DbContextOptionsBuilder options, IConfiguration configuration)
        {
            bool isDev = configuration["ENVIRONMENT"]?.Equals("Development") ?? false;
            isDev |= configuration["ASPNETCORE_ENVIRONMENT"]?.Equals("Development") ?? false;
            if (isDev)
            {
                options.EnableSensitiveDataLogging(true);
            }

            return options;
        }
    }
}
