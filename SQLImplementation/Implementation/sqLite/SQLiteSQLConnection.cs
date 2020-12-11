// <copyright file="SQLiteSQLConnection.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using SQLImplementation.contracts;

namespace SQLImplementation.Implementation.sqLite
{
    public class SqLiteSqlConnection : ISqlConnection
    {
        private string dataBaseLocation;
        /// <summary>
        /// DatabaseLocation (.db-File) will be checked if valid and exists.
        /// </summary>
        public string DataBaseLocation
        {
            get => dataBaseLocation;
            set => dataBaseLocation = ValidateLocation(value);
        }

        private SQLiteConnection connection;

        public SqLiteSqlConnection(String databaseLocation)
        {
            DataBaseLocation = ValidateLocation(databaseLocation);
        }

        private string ValidateLocation(String location)
        {
            string toRet = null;
            try
            {

                var uri = new Uri(location);
                if (File.Exists(uri.ToString()))
                {
                    toRet = uri.ToString();
                }
            }
            catch (UriFormatException ex)
            {
                Console.Error.WriteLine(
                    $"The input was malformed, please check and correct the database location input: {location}");
                PrintMessageAndStackTrace(ex);
            }
            catch (ArgumentNullException ex1)
            {
                Console.Error.WriteLine(
                    $"The input was null, please provide a database location input");
                PrintMessageAndStackTrace(ex1);
            }

            return toRet;

        }

        private static void PrintMessageAndStackTrace(Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            Console.Error.WriteLine(ex.StackTrace);
        }


        public ISqlConnection InitializeConnection()
        {
            if (dataBaseLocation == null)
            {
                Console.Error.WriteLine("Database location is not initialized, cannot connect to database");
            }
            else
            {
                connection = new SQLiteConnection(dataBaseLocation);
            }

            return this;
        }

        public ISqlConnection Connect()
        {
            if (connection == null)
            {
                InitializeConnection();
            }
            connection?.Open();
            return this;
        }

        public DbConnection GetConnection()
        {
            return connection;
        }
    }
}
