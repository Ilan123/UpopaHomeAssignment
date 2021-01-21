using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBoundsArea : MonoBehaviour
{
    public static WorldBoundsArea instance;
    public BoxCollider worldAreaCol;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        worldAreaCol = GetComponent<BoxCollider>();
        instance = this;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Spaceship")
        {
            Destroy(other.gameObject);
        }
        else if (other.tag == "Spawnable")
        {
            other.GetComponent<Spawnable>().Destroy(true);
        }
    }

    public bool IsInWordBounrds(Vector3 pos)
    {
        return worldAreaCol.bounds.Contains(pos);
    }
}
