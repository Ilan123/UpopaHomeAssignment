using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLogic : MonoBehaviour
{
    public System.Action<float> onGettingHitted = delegate { };
    public void GettingHit(float damage)
    {
        onGettingHitted(damage);
    }
}
