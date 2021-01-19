using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBoundsLogic : MonoBehaviour
{
    BoxCollider boxCol;
    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        InitBordersRange();
    }

    private void InitBordersRange()
    {
        (float, float) screenDimentions = CalcTool.GetScreenSize();
        float frustumWidth = screenDimentions.Item1;
        float frustumHeight = screenDimentions.Item2;

        boxCol.center = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        boxCol.size = new Vector3(frustumWidth, frustumHeight, 1f);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("DD");
        if (other.tag == "Spaceship")
        {
            Debug.Log("DD2");
            Destroy(other.gameObject);
        }
        else if (other.tag == "Spawnable")
        {
            other.GetComponent<Spawnable>().Destroy();
        }
    }
}
