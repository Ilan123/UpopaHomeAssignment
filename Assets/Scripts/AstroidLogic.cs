using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AstroidLogic : MonoBehaviour, Spawnable
{
    public System.Action onDestroy = delegate { };
    [SerializeField] public float damageOnImpact;
    // This astroid has a generic spawn system, it can spawn one object according to distribution, or spawn all
    // For futhure upgrades when we can create an explotion but also to spawn a pickable item
    [SerializeField] bool spawnAll = false;
    [SerializeField] public GameObject[] objectsToRespawnOnDestroyPrefs;
    [SerializeField] float[] spawnDistribution = null;
    [SerializeField] public int spawnableObjetsType;
    [SerializeField] float lifeTime = 8f;
    [SerializeField] float movmentSpeed = 0f;
    public Vector3 _randomMovingDirection = Vector3.zero;
    private float _spawnTime = 0;
    

    public int GetSpawnableObjetsType() => spawnableObjetsType;
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _spawnTime > lifeTime)
        {
            Destroy(true);
        }
        transform.position = transform.position + (_randomMovingDirection * Time.deltaTime);
    }

    public GameObject Duplicate()
    {
        GameObject dup = SpawnsPoolManager.instance.SpawnableObject(gameObject);
        dup.GetComponent<AstroidLogic>()._randomMovingDirection = _randomMovingDirection;

        return dup;
    }

    public GameObject Init()
    {
        _spawnTime = Time.time;
        // creating a random vector with magnitude correlated to the speed
        if (_randomMovingDirection == Vector3.zero)
        {
            _randomMovingDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized * Mathf.Sqrt(movmentSpeed);
        }
        return gameObject;
    }

    public void Destroy(bool isDestroyedByBoundry)
    {
        onDestroy();
        if (!isDestroyedByBoundry && WorldBoundsArea.instance.IsInWordBounrds(transform.position))
        {
            if (spawnAll)
            {
                foreach (GameObject gameObjectToInit in objectsToRespawnOnDestroyPrefs)
                {
                    GameObject spawnedObj = SpawnsPoolManager.instance.SpawnableObject(gameObjectToInit);
                    spawnedObj.transform.position = transform.position;
                }
            }
            else
            {
                GameObject spawnedObj = null;
                int indexToSpawn = CalcTool.GetNumberAccordingToDistribution(spawnDistribution);
                if (indexToSpawn != -1)
                {
                    spawnedObj = SpawnsPoolManager.instance.SpawnableObject(objectsToRespawnOnDestroyPrefs[indexToSpawn]);
                    spawnedObj.transform.position = transform.position;
                }
            }
        }

        SpawnsPoolManager.instance.AddUnactivateObject(this);
        gameObject.SetActive(false);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Spaceship")
        {
            collision.gameObject.GetComponent<HealthLogic>().GettingHit(damageOnImpact);
            Destroy(false);
        }
    }

}
