// <copyright file="ISqlStatementEnd.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;

namespace SQLImplementation.contracts.sqlQuery.queryparts
{
    /// <summary>
    /// This interface oblige the coder to write a CreateStatement method wherever necessary
    /// </summary>
    public interface ISqlStatementEnd
    {
       /// <summary>
       /// Converts the preceding statement to a String
       /// </summary>
       /// <returns><see cref="String"/>, consisting of the build statement </returns>
        public String CreateStatement();
    }
}
