using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAddHealth : SpawnableObject
{
    System.Action onAddingHealth = delegate { };
    [SerializeField] int amount = 0;

    public void AddHealth()
    {
        GameManager.instance.AddHealth(amount);
    }

    public override void Destroy(bool isDestroyedByBoundry)
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
