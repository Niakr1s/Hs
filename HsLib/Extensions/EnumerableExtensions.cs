﻿namespace HsLib.Extensions
{
    internal static class EnumerableExtensions
    {
        internal static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
        {
            return source.Select((item, index) => (item, index));
        }
    }
}
