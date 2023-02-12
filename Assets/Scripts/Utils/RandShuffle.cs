using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandShuffle<T>
{
	public T type;

    public static List<T> Shuffle(List<T> aList)
    {
        System.Random _random = new System.Random();

        T myGO;

        int n = aList.Count;
        for (int i = 0; i < n; i++)
        {
            // NextDouble returns a random number between 0 and 1.
            // ... It is equivalent to Math.random() in Java.
            int r = i + (int)(_random.NextDouble() * (n - i));
            myGO = aList[r];
            aList[r] = aList[i];
            aList[i] = myGO;
        }

        return aList;
    }
}