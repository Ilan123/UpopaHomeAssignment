using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class SpawnableObject : MonoBehaviour, Spawnable
{
    [SerializeField] public int spawnableObjetsType;
    public int GetSpawnableObjetsType() => spawnableObjetsType;
    public void SetSpawnableObjectType(int typeId) => spawnableObjetsType = typeId;
    public virtual GameObject Init()
    {
        return gameObject;
    }

    public virtual GameObject Duplicate()
    {
        GameObject dup = SpawnsPoolManager.instance.SpawnableObject(gameObject);
        dup.transform.position = transform.position;
        dup.transform.rotation = transform.rotation;
        return dup;
    }

    public virtual void Destroy(bool isDestroyedByBoundry)
    {
        SpawnsPoolManager.instance.AddUnactivateObject(this);
        gameObject.SetActive(false);
    }
}
