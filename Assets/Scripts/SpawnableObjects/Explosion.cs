using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : SpawnableObject
{
    // both the explosion and the astroid does damage
    [SerializeField] public float damage = 10;
    [SerializeField] float radius = 1;
    [SerializeField] float delay = 0;
    [SerializeField] LayerMask layerToDestroy = 0;
    [SerializeField] GameObject explosionEffect = null;

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
        StartCoroutine(DestroyObjectsAroundAfterDelay());
    }

    private IEnumerator DestroyObjectsAroundAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Collider[] objectsToDestroy = Physics.OverlapSphere(transform.position, radius, layerToDestroy);
        foreach (Collider obj in objectsToDestroy)
        {
            if (obj.tag == "Spaceship")
            {
                obj.GetComponent<HealthLogic>().GettingHit(damage);
            }
            else if (obj.tag == "Spawnable")
            {
                obj.GetComponent<Spawnable>().Destroy(false);
            }
        }
        Destroy(false);
    }

}
