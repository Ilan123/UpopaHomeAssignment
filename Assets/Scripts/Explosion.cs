using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, Spawnable
{
    [SerializeField] public int spawnableObjetsType;
    // both the explosion and the astroid does damage
    [SerializeField] public float damage = 10;
    [SerializeField] float radius = 1;
    [SerializeField] float delay = 0;
    [SerializeField] LayerMask layerToDestroy = 0;
    [SerializeField] GameObject explosionEffect = null;
    public int GetSpawnableObjetsType() => spawnableObjetsType;
    private void Start()
    {
        explosionEffect.transform.localScale = transform.localScale * (radius / 2);
    }
    private void OnEnable()
    {
        Explotion();
    }
    private void Explotion()
    {

        Debug.Log("Explotion");
        StartCoroutine(DestroyObjectsAroundAfterDelay());
    }

    private IEnumerator DestroyObjectsAroundAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        //Instantiate(test);
        Debug.Log("Explotion2");
        Collider[] objectsToDestroy = Physics.OverlapSphere(transform.position, radius, layerToDestroy);
        Debug.Log("Got: " + objectsToDestroy.Length);
        foreach (Collider obj in objectsToDestroy)
        {
            Debug.Log("Got some things");
            if (obj.tag == "Spaceship")
            {
                obj.GetComponent<HealthLogic>().GettingHit(damage);
            }
            else if (obj.tag == "Spawnable")
            {
                obj.GetComponent<Spawnable>().Destroy();
            }
        }
        Destroy();
    }
    public GameObject Init()
    {
        return gameObject;
    }
    public void Destroy()
    {
        SpawnsPoolManager.instance.AddUnactivateObject(this);
        gameObject.SetActive(false);
    }

}
