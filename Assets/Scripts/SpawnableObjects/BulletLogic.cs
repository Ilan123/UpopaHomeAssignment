using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : SpawnableObject
{
    [SerializeField] float lifeTime = 8f;
    private float _spawnTime = 0;
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

    public override GameObject Duplicate()
    {
        GameObject dup = base.Duplicate();
        
        dup.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
        dup.GetComponent<BulletLogic>().SetSpawnTime(_spawnTime);

        return dup;
    }

    public override GameObject Init() {
        _spawnTime = Time.time;
        return gameObject;
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
