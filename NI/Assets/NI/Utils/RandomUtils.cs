using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUtils : MonoBehaviour
{
    public static double randomFloat(double minInclusive, double maxInclusive, double precision)
    {
        int max = (int)(maxInclusive / precision);
        int min = (int)(minInclusive / precision);
        int randomInt = Random.Range(min, max);
        double randomNum = randomInt * precision;
        return randomNum;
    }

    public static int getRandomNumber(int min, int max)
    {
        return (int)Random.Range(min, max);
    }
}
