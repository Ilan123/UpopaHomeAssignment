using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour, Spawnable
{
    [SerializeField] public int spawnableObjetsType;
    [SerializeField] float lifeTime = 8f;
    private float _spawnTime = 0;

    public int GetSpawnableObjetsType() => spawnableObjetsType;
    void Start()
    {
        _spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - _spawnTime > lifeTime)
        {
            Destroy();
        }
    }

    public GameObject Init() {
        _spawnTime = Time.time;
        return gameObject;
    }

    public void Destroy()
    {
        SpawnsPoolManager.instance.AddUnactivateObject(this);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (1 << other.gameObject.layer == LayerMask.GetMask("Destroyable"))
        {
            other.GetComponent<Spawnable>().Destroy();
        }
    }
}
