using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CalcTool
{
    static float screenWidth = 0;
    static float screenHeight = 0;

    /// <summary>
    /// Getting a random number according to the given distribution
    /// array numbers need to sum to a number between 0 to 1
    /// </summary>
    /// <param name="distribution"></param>
    /// <returns></returns>
    public static int GetNumberAccordingToDistribution(float[] distribution)
    {
        float randNum = Random.Range(0, 1f);
        float cdf = 0;
        // We check if the random number is between the the i-1 number probability to the i number probability
        // if it is we would return it.
        // we calc a cdf to know from which part of the distribution the choosen number come from.
        for (int i = 0; i < distribution.Length; i++)
        {
            cdf += distribution[i];
            if (randNum <= cdf)
            {
                return i;
            }
        }
        return -1;
    }
    /// <summary>
    /// Calculate the screen sizes in world scale
    /// </summary>
    /// <returns>return tuple: (screenWidth, screenHeight)</returns>
    public static (float, float) GetScreenSize()
    {
        if (screenWidth == 0 || screenHeight == 0)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            float frustumHeight = 2f * Camera.main.orthographicSize;
            float frustumWidth = frustumHeight * Camera.main.aspect;

            screenWidth = frustumWidth;
            screenHeight = frustumHeight;
        }

        return (screenWidth, screenHeight);
    }

}
