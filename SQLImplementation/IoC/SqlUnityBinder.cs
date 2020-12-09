// <copyright file="UnityBinder.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using SQLImplementation.contracts;
using SQLImplementation.Implementation;
using Unity;

namespace SQLImplementation.IoC
{
    public class SqlUnityBinder
    {
        public static IUnityContainer bind(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<ISqlConnection, SqLiteSqlConnection>();

            return unityContainer;
        }
    }
}
