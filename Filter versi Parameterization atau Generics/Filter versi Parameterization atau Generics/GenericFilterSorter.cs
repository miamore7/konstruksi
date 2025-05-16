using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter_versi_Parameterization_atau_Generics
{
    public static class GenericFilterSorter
    {
        // Fungsi generik untuk filter
        public static List<T> FilterBy<T>(List<T> list, Func<T, bool> predicate)
        {
            return list.Where(predicate).ToList();
        }

        // Fungsi generik untuk sort
        public static List<T> SortBy<T, TKey>(List<T> list, Func<T, TKey> keySelector, bool ascending = true)
        {
            return ascending
                ? list.OrderBy(keySelector).ToList()
                : list.OrderByDescending(keySelector).ToList();
        }
    }
}
