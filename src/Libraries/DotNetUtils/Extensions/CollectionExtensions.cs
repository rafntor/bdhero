﻿// Copyright 2012-2014 Andrew C. Dvorak
//
// This file is part of BDHero.
//
// BDHero is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// BDHero is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with BDHero.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetUtils.Extensions
{
    /// <see href="http://stackoverflow.com/a/2984664/467582"/>
    public static class CollectionExtensions
    {
        #region Joining

        public static string Join<T>(this IEnumerable<T> collection, string delimiter = "")
        {
            return string.Join(delimiter, collection);
        }

        public static string JoinLines<T>(this IEnumerable<T> collection)
        {
            return string.Join(Environment.NewLine, collection);
        }

        #endregion

        #region Indentation

        public static string Indent<TItem>(this IEnumerable<TItem> enumerable, int numSpaces = 4)
        {
            var padding = new string(' ', numSpaces);
            return enumerable.Select(item => string.Format("{0}{1}", padding, item)).JoinLines();
        }

        public static string IndentTrim<TItem>(this IEnumerable<TItem> enumerable, int numSpaces = 4)
        {
            var padding = new string(' ', numSpaces);
            return enumerable.Select(item => item.ToString())
                             .Where(line => !string.IsNullOrWhiteSpace(line))
                             .Select(item => string.Format("{0}{1}", padding, item)).JoinLines();
        }

        #endregion

        /// <summary>
        /// Adds all elements in the specified enumerable to this collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="newItems">Items to add to this collection</param>
        /// <typeparam name="T"></typeparam>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> newItems)
        {
            foreach (T item in newItems)
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// Inserts the specified <paramref name="items"/> at the beginning of the list (in order) and returns the mutated list.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="items"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> Prepend<T>(this IList<T> list, params T[] items)
        {
            for (var i = items.Length; i >= 0; --i)
            {
                list.Insert(0, items[i]);
            }
            return list;
        }

        /// <summary>
        /// Iterates over each item in the collection and applies the specified action to it.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="action">Operation to invoke on each item in the collection</param>
        /// <typeparam name="T"></typeparam>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T item in collection)
            {
                action(item);
            }
        }

        /// <summary>
        /// Iterates over each item in the collection and applies the specified action to it.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="action">Operation to invoke on each item in the collection</param>
        /// <typeparam name="T"></typeparam>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T, bool> action)
        {
            var items = collection as T[] ?? collection.ToArray();
            var i = 0;
            var len = items.Count();
            foreach (T item in items)
            {
                var isLast = ++i == len;
                action(item, isLast);
            }
        }

        /// <summary>
        /// Iterates over each item in the collection and applies the specified action to it.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="action">Operation to invoke on each item in the collection</param>
        /// <typeparam name="T"></typeparam>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            var i = 0;
            foreach (T item in collection)
            {
                action(item, i++);
            }
        }

        /// <summary>
        /// Iterates over each item in the collection and applies the specified action to it.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="action">Operation to invoke on each item in the collection</param>
        /// <typeparam name="T"></typeparam>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T, int, int> action)
        {
            var items = collection as T[] ?? collection.ToArray();
            var i = 0;
            var len = items.Count();
            foreach (T item in items)
            {
                action(item, i++, len);
            }
        }

        /// <summary>
        /// Converts the Enumerable to a MultiValueDictionary using the specified key provider function.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="values"></param>
        /// <param name="keyProvider"></param>
        /// <returns></returns>
        public static MultiValueDictionary<TKey, TValue> ToMultiValueDictionary<TKey, TValue>(this IEnumerable<TValue> values, Func<TValue, TKey> keyProvider)
        {
            var dic = new MultiValueDictionary<TKey, TValue>();
            foreach (var value in values)
            {
                dic.Add(keyProvider(value), value);
            }
            return dic;
        }
    }
}