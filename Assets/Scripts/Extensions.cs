using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static int ToInt(this bool boolean)
    {
        return boolean ? 1 : 0;
    }

    public static void Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        for(int i = n-1;i>0;i--)
        {
            int k = Random.Range(0,i+1); // Unity version
            (list[i], list[k])=(list[k], list[i]);
        }
    }
}