﻿using System.Collections;
using System.Collections.Generic;

namespace MenuPlanner.Shared.Extension
{
    public static class ICollectionExtension
    {
        public static void AddIfNotContains<T>(this ICollection<T> collection, T item) where T:class
        {
            if (!collection.Contains(item))
            {
                collection.Add(item);
            }
          
        }
    }
}