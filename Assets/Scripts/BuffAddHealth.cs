using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAddHealth : MonoBehaviour, Spawnable
{
    [SerializeField] public int spawnableObjetsType = 0;
    System.Action onAddingHealth = delegate { };
    [SerializeField] int amount = 0;
    public int GetSpawnableObjetsType() => spawnableObjetsType;
    public void AddHealth()
    {
        GameManager.instance.AddHealth(amount);
    }

    public GameObject Duplicate()
    {
        GameObject dup = SpawnsPoolManager.instance.SpawnableObject(gameObject);

        return dup;
    }

    public GameObject Init()
    {
        return gameObject;
    }

    public void Destroy(bool isDestroyedByBoundry)
    {
        SpawnsPoolManager.instance.AddUnactivateObject(this);
        gameObject.SetActive(false);
    }
    /*
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Spaceship")
        {
            onAddingHealth();
            GameManager.instance.AddHealth(amount);
            Destroy(false);
        }
    }
    */
    public void OnMouseDown()
    {
        onAddingHealth();
        GameManager.instance.AddHealth(amount);
        Destroy(false);
    }
}
