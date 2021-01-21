using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Spawnable
{
    int GetSpawnableObjetsType();
    void SetSpawnableObjectType(int typeId);
    GameObject Init();
    void Destroy(bool isDestroyedByBoundry);

    GameObject Duplicate();
}
