using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CalcTool
{
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
        Vector3 cameraPos = Camera.main.transform.position;
        float frustumHeight = 2f * Camera.main.orthographicSize;
        float frustumWidth = frustumHeight * Camera.main.aspect;

        return (frustumWidth, frustumHeight);
    }

}
