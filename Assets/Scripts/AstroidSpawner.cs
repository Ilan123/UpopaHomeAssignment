using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawner : MonoBehaviour
{
    // The distribution of the probability to spawn each type of astroid.
    // If the totl is less than 1, then (1 - total) is the probability to not spawn any astroid.
    // IMPORTANT the distribution probabilies should sum to a number between 0 to 1.
    [SerializeField] GameObject[] astroidsTypes = null;
    [SerializeField] float[] spawnDistribution = null;
    [SerializeField] float adstroidSpawnRate = 1f;
    float _lastSpawnTime;
    private float xMinValue = 0;
    private float xMaxValue = 0;
    private float yMinValue = 0;
    private float yMaxValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        _lastSpawnTime = Time.time;
        float probsSum = 0;
        foreach (float prob in spawnDistribution)
        {
            probsSum += prob;
        }
        if (probsSum > 1)
        {
            Debug.LogWarning("The distribution probabilies sums to more than 1");
        }
        InitSpawnRange();
    }

    void Update()
    {
        if (Time.time - _lastSpawnTime > adstroidSpawnRate)
        {
            _lastSpawnTime = Time.time;
            SpawnAstroidWithRespecdToDistribution();
        }
    }

    /// <summary>
    /// Return a random astroid with respect to the defined distribution probabilies
    /// </summary>
    /// <returns>GameObject of Astroid Prefab, or null if none was selected</returns>
    private GameObject SpawnAstroidWithRespecdToDistribution()
    {
        GameObject astroid = null;
        int indexToSpawn = CalcTool.GetNumberAccordingToDistribution(spawnDistribution);
        if(indexToSpawn != -1)
        {
            astroid = SpawnsPoolManager.instance.SpawnableObject(astroidsTypes[indexToSpawn]);
            InitAstroid(astroid);
            return astroid;
        }
        return astroid;
    }

    private void InitSpawnRange()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        float frustumHeight = 2f * Camera.main.orthographicSize;
        float frustumWidth = frustumHeight * Camera.main.aspect;

        xMinValue = cameraPos.x - frustumWidth / 2;
        xMaxValue = cameraPos.x + frustumWidth / 2;
        yMinValue = cameraPos.y - frustumHeight / 2;
        yMaxValue = cameraPos.y + frustumHeight / 2;
    }

    private void InitAstroid(GameObject astroid)
    {
        astroid.transform.position = new Vector3(Random.Range(xMinValue, xMaxValue), Random.Range(yMinValue, yMaxValue), 0);
    }
}
