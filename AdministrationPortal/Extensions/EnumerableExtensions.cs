using System;
using System.Collections.Generic;
using System.Linq;

namespace AdministrationPortal.Extensions
{
    public static class EnumerableExtensions
    {

        /// <summary>
        /// Converts an Enumerable into a string of comma-separated values
        /// </summary>
        public static string ToCSV<T>(this IEnumerable<T> enumerable)
        {
            var csv = "";
            if (enumerable != null && enumerable.Any())
            {
                foreach (var str in enumerable)
                {
                    csv += str + ", ";
                }
                csv = csv.Remove(csv.LastIndexOf(','), 1);
            }
            return csv;
        }

        /// <summary>
        /// Gets the unique elements in an Enumerable
        /// </summary>
        public static IEnumerable<T> GetUnique<T>(this IEnumerable<T> t)
        {
            var uniqueThings = new HashSet<T>();
            foreach (var thing in t)
                uniqueThings.Add(thing);
            return uniqueThings;
        }
    }
}