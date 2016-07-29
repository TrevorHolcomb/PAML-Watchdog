using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministrationPortal.Extensions
{
    public static class IEnumerableExtensions
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
    }
}