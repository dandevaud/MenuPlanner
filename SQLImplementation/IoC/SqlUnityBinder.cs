// <copyright file="SqlUnityBinder.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using SQLImplementation.contracts;
using SQLImplementation.Implementation.sqLite;
using Unity;

namespace SQLImplementation.IoC
{
    public class SqlUnityBinder
    {
        public static IUnityContainer bindSqLite(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<ISqlConnection, SqLiteSqlConnection>();

            return unityContainer;
        }

        [InjectionConstructor]
        public SqlUnityBinder(IUnityContainer unityContainer)
        {
           
        }
    }
}
