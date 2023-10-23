using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Shun_Utility
{
    public static class SetOperations
    {
        // Union function to return the union of two lists
        public static List<T> Union<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {
            return list1.Union(list2).ToList();
        }

        // Set difference function to return the elements that are in list1 but not in list2
        public static List<T> SetDifference<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {
            return list1.Except(list2).ToList();
        }

        // Intersection function to return the common elements in both lists
        public static List<T> Intersection<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {
            return list1.Intersect(list2).ToList();
        }
        
        public static T[] MergeArrays<T>(T[] array1, T[] array2)
        {
            if (array1 == null || array1.Length == 0)
                return array2;

            if (array2 == null || array2.Length == 0)
                return array1;

            int mergedLength = array1.Length + array2.Length;
            T[] mergedArray = new T[mergedLength];

            Array.Copy(array1, 0, mergedArray, 0, array1.Length);
            Array.Copy(array2, 0, mergedArray, array1.Length, array2.Length);

            return mergedArray;
        }
        
        public static T[] MergeArrays<T>(params T[][] arrays)
        {
            // Calculate the total length of the merged array
            int totalLength = arrays.Sum(arr => arr.Length);

            // Create a new array to hold the merged elements
            T[] mergedArray = new T[totalLength];

            int currentIndex = 0;

            // Copy elements from each array to the merged array
            foreach (T[] arr in arrays)
            {
                Array.Copy(arr, 0, mergedArray, currentIndex, arr.Length);
                currentIndex += arr.Length;
            }

            return mergedArray;
        }
        
        
        // Shuffle a list using the Fisher-Yates shuffle algorithm
        public static List<T> ShuffleList<T>(IEnumerable<T> list)
        {
            var collection = list as T[] ?? list.ToArray();
            List<T> shuffledList = new List<T>(collection);
            int n = collection.Count();
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n + 1);
                (shuffledList[k], shuffledList[n]) = (shuffledList[n], shuffledList[k]);
            }

            return shuffledList;
        }
    }
}