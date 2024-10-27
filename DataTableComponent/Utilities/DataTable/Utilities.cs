using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;

namespace DataTableComponent.Utilities.DataTable
{
    public static class Utilities
    {
        public static List<List<List<T>>>? ChunkeateList<T>(this List<List<T>> list, int chunkSize, out int necessaryChunks)
        {
            necessaryChunks = 0;
            //this kind of arguments don't make sense
            if (list == null || list.Count == 0 || chunkSize <= 0)
            {
                return null;
            }

            var chunkedList = new List<List<List<T>>>();
            necessaryChunks = (int)Math.Ceiling((double)list.Count / (double)chunkSize);
            for (int i = 0; i < necessaryChunks; i++)
            {
                chunkedList.Add(list.Skip(i * chunkSize).Take(chunkSize).ToList());
            }

            return chunkedList;
        }

        public static List<List<string>> FilterByTextMatching(this List<List<string>> list, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return list;
            }

            var filteredList = new List<List<string>>();
            foreach (var subList in list)
            {
                foreach (var value in subList)
                {
                    if (value.ToLower().Contains(text.ToLower()))
                    {
                        filteredList.Add(subList);
                        break;
                    }
                }

            }

            return filteredList;
        }

        public static List<List<string>> OrderMatrixByColumn(this List<List<string>> list, int orderedColumn, bool orderUp)
        {
            if (list == null || list.Count == 0 || list.All(subList => orderedColumn >= subList.Count))
            {
                return list;
            }

            var orderedList = list
                .Where(subList => orderedColumn < subList.Count) // Ignore lists that are too short to contain the ordered column
                .OrderBy(subList => subList[orderedColumn], StringComparer.Ordinal)
                .ToList();

            if (!orderUp)
            {
                orderedList.Reverse(); // Reverse for descending order if needed
            }

            return orderedList;
        }
    }
}
