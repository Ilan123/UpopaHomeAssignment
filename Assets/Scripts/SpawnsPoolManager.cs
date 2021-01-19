using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnsPoolManager : MonoBehaviour
{
    public static SpawnsPoolManager instance;
    // this array of lists of the spawnable gameobjects 
    private Dictionary<int, List<Spawnable>> _unactivedSpawnableObjects; // pool



    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
    }

    private void Start()
    {
        InitSpawnableList();
    }

    /// <summary>
    /// Init the array of spawnable objects lists.
    /// Creating a list foreach spawnable prefeb, and save it in the array.
    /// </summary>
    private void InitSpawnableList()
    {
        _unactivedSpawnableObjects = new Dictionary<int, List<Spawnable>>();
    }

    /// <summary>
    /// Checking if there an available (unactive) object to use.
    /// If there is, init it and return the object.
    /// If there is none, instantiate a new one.
    /// </summary>
    /// <param name="spawnableType">The type of the spawnable object</param>
    /// <returns>A gameobject of an astroid that has been added to the scene</returns>
    public GameObject SpawnableObject(GameObject objectToSpawn)
    {
        Spawnable spawnComp;
        if (objectToSpawn.TryGetComponent<Spawnable>(out spawnComp))
        {
            int typeIndex = spawnComp.GetSpawnableObjetsType();
            // If i had more time I would make a more effient way, that create an array of lists for all the spawnable objects.
            if (!_unactivedSpawnableObjects.ContainsKey(typeIndex))
            {
                _unactivedSpawnableObjects.Add(typeIndex, new List<Spawnable>());
            }

            if (_unactivedSpawnableObjects[typeIndex].Count > 0)
            {
                GameObject spawnableObject = _unactivedSpawnableObjects[typeIndex][0].Init();
                _unactivedSpawnableObjects[typeIndex].RemoveAt(0);
                spawnableObject.SetActive(true);
                return spawnableObject;
            }
        }
        return Instantiate(objectToSpawn);
    }


    public void AddUnactivateObject(Spawnable spawnableObject)
    {
        int typeIndex = spawnableObject.GetSpawnableObjetsType();
        if (!_unactivedSpawnableObjects.ContainsKey(typeIndex))
        {
            _unactivedSpawnableObjects.Add(typeIndex, new List<Spawnable>());
        }
        _unactivedSpawnableObjects[typeIndex].Add(spawnableObject);
    }

}
