using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnsPoolManager : MonoBehaviour
{
    public static SpawnsPoolManager instance;
    // The object that we want to manage with the pool, auto assign type id to each object prefab.
    [SerializeField] GameObject[] spawnablePrefabs = null;
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
        for (int i = 0; i < spawnablePrefabs.Length; i++)
        {
            spawnablePrefabs[i].GetComponent<Spawnable>().SetSpawnableObjectType(i);
            _unactivedSpawnableObjects.Add(i, new List<Spawnable>());
        }
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
        if (_unactivedSpawnableObjects.ContainsKey(typeIndex))
        {
            _unactivedSpawnableObjects[typeIndex].Add(spawnableObject);
        }
        else
        {
            Debug.LogWarning("Tried to spawn a spawnable object, that its prefab werenn't added to the pref pool array, please add the prefab to the SpawnsPoolManager prefabs array");
        }
    }

}
