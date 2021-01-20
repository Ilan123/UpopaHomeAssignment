using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour, Spawnable
{
    [SerializeField] public int spawnableObjetsType;
    [SerializeField] float lifeTime = 8f;
    private float _spawnTime = 0;

    public int GetSpawnableObjetsType() => spawnableObjetsType;
    public void SetSpawnTime(float time) => _spawnTime = time;
    void Start()
    {
        if(_spawnTime == 0)
        {
            _spawnTime = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - _spawnTime > lifeTime)
        {
            Destroy(true);
        }
    }

    public GameObject Duplicate()
    {
        GameObject dup = SpawnsPoolManager.instance.SpawnableObject(gameObject);
        
        dup.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
        dup.transform.position = transform.position;
        dup.transform.rotation = transform.rotation;
        dup.GetComponent<BulletLogic>().SetSpawnTime(_spawnTime);

        return dup;
    }

    public GameObject Init() {
        _spawnTime = Time.time;
        return gameObject;
    }

    public void Destroy(bool isDestroyedByBoundry)
    {
        SpawnsPoolManager.instance.AddUnactivateObject(this);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == LayerMask.GetMask("Destroyable") && other.tag != "Spaceship")
        {
            other.GetComponent<Spawnable>().Destroy(false);
            Destroy(false);
        }
    }
}
