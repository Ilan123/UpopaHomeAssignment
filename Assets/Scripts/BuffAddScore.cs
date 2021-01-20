using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAddScore : MonoBehaviour, Spawnable
{
    [SerializeField] public int spawnableObjetsType;
    System.Action onAddingScore = delegate { };
    [SerializeField] int amount = 0;
    public int GetSpawnableObjetsType() => spawnableObjetsType;
    public void AddScore()
    {
        GameManager.instance.AddScore(amount);
    }

    public GameObject Init()
    {
        return gameObject;
    }

    public GameObject Duplicate()
    {
        GameObject dup = SpawnsPoolManager.instance.SpawnableObject(gameObject);

        return dup;
    }

    public void Destroy(bool isDestroyedByBoundry)
    {
        SpawnsPoolManager.instance.AddUnactivateObject(this);
        gameObject.SetActive(false);
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered HotDog");
        if (other.tag == "Spaceship")
        {
            Debug.Log("Triggered HotDog - 2");
            onAddingScore();
            GameManager.instance.AddScore(amount);
            Destroy(false);
        }
    }
    */
    public void OnMouseDown()
    {
        Debug.Log("Triggered HotDog - 2");
        onAddingScore();
        GameManager.instance.AddScore(amount);
        Destroy(false);
    }
}
