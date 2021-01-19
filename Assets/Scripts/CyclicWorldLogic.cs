using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CyclicWorldLogic : MonoBehaviour
{
    [SerializeField] GameObject spaceshipPref = null;
    [SerializeField] RectTransform rightPanelReactTran = null;
    [SerializeField] RectTransform leftPanelReactTran = null;
    BoxCollider boxCol;
    private float _spaceshipSize = 0f;

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        _spaceshipSize = spaceshipPref.GetComponent<MeshFilter>().sharedMesh.bounds.size.magnitude * 0.75f;
        InitBordersRange();
    }

    private void InitBordersRange()
    {
        (float, float) screenDimentions = CalcTool.GetScreenSize();
        float frustumWidth = screenDimentions.Item1;
        float frustumHeight = screenDimentions.Item2;

        Debug.Log("spaceshipSize: " + _spaceshipSize);
        boxCol.center = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        boxCol.size = new Vector3(frustumWidth - _spaceshipSize, frustumHeight - _spaceshipSize, 1f);
    }

    private void OnTriggerExit(Collider spaceship)
    {
        float crossWidht = 0f;
        if(spaceship.tag == "Spaceship")
        {
            Vector3 originalSpaceshipPos = spaceship.transform.position;
            Vector3 direction = boxCol.center - originalSpaceshipPos;
            float angel = Mathf.Atan(Mathf.Abs(direction.y / direction.x)) * Mathf.Rad2Deg;
            Vector3 newSpaceshipPos = -originalSpaceshipPos;
            Debug.Log("Angel: " + angel);
            // not corner cases:
            if ((angel >= 0 && angel < (45 - crossWidht)) || (angel > (325 + crossWidht) && angel < 360))
            {
                Debug.Log("1");
                newSpaceshipPos = new Vector3(-(originalSpaceshipPos.x + _spaceshipSize), originalSpaceshipPos.y, 0);
            }
            else if(angel > (45 + crossWidht) && angel < (135 - crossWidht))
            {
                Debug.Log("2");
                newSpaceshipPos = new Vector3(originalSpaceshipPos.x, -originalSpaceshipPos.y, 0);
            }
            else if(angel > (135 + crossWidht) && angel < (270 - crossWidht))
            {
                Debug.Log("3");
                newSpaceshipPos = new Vector3(-(originalSpaceshipPos.x + _spaceshipSize), originalSpaceshipPos.y, 0);
            }
            else if((angel > (270 + crossWidht) && angel < (315 - crossWidht)))
            {
                Debug.Log("4");
                newSpaceshipPos = new Vector3(originalSpaceshipPos.x, originalSpaceshipPos.y, 0);
            }
            else
            {
                Debug.Log("5");
            }

            Instantiate(spaceshipPref, newSpaceshipPos, spaceship.transform.rotation);
            
        }
    }



}
