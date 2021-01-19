using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface Spawnable
{
    int GetSpawnableObjetsType();
    GameObject Init();
    void Destroy();
}
