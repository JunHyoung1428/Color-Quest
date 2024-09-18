using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public static int[] RandomIndex<T>(this List<T> list)
    {
        int[] index = new int[list.Count];
        for(int i = 0; i < list.Count; i++)
        {
            index[i] = i;
        }

        for (int i = 0; i < index.Length; i++)
        {
            int randomIndex = Random.Range(i, index.Length);
            int temp = index[i];
            index[i] = index[randomIndex];
            index[randomIndex] = temp;
        }
        return index;
    }
}
