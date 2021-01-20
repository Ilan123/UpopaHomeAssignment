using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBorder : MonoBehaviour
{
    [SerializeField] Vector2 direction = Vector2.zero;
    [SerializeField] Collider colToIgnore; // the collider of the boundry in the other size
    BoxCollider boxCol = null;
    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
    }

    public void OnTriggerEnter(Collider obj)
    {
        StartCoroutine(SpawnMirror(obj));
    }


    /// <summary>
    /// Checking if the object is moving toward the bound, if so, generating a copy mirror,
    /// at the other side
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public IEnumerator SpawnMirror(Collider obj)
    {
        // preventing objected that are off the screen to be mirrored
        if (!WorldBoundsArea.instance.IsInWordBounrds(obj.transform.position))
        {
            yield break;
        }
            
        Vector3 prevDist = transform.position - obj.transform.position;
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Vector3 currentDist = transform.position - obj.transform.position;
        float dist = 0;

        // we are checking if the object is mobing toward the border

        if (direction.y != 0)
        {
            dist = direction.y*(prevDist.y - currentDist.y);
        }
        else if(direction.x != 0)
        {
            dist = direction.x*(prevDist.x - currentDist.x);
        }

        // the porpose  of this condition,
        // is to identify objects that spawn within the collider
        Debug.Log("dist: " + dist);
        if (dist > 0)
        {
            
            GameObject mirorObj = null;
            Vector3 mirrorObjPos = Vector3.zero;

            // We are doing calc of the mirror object position based on the border direction, and the screen dimentions.
            float xAbsDirection = Mathf.Abs(direction.x);
            float yAbsDirection = Mathf.Abs(direction.y);
            float xMirrorPos = ((1 - xAbsDirection) * obj.transform.position.x) + (xAbsDirection * (obj.transform.position.x - direction.x*WorldBoundsArea.instance.worldAreaCol.bounds.size.x));
            float yMirrorPos = ((1 - yAbsDirection) * obj.transform.position.y) + (yAbsDirection * (obj.transform.position.y - direction.y*WorldBoundsArea.instance.worldAreaCol.bounds.size.y));

            mirrorObjPos = new Vector3(xMirrorPos, yMirrorPos, 0);

            if (obj.tag == "Spawnable")
            {

                mirorObj = obj.GetComponent<Spawnable>().Duplicate();
                mirorObj.transform.position = mirrorObjPos;
            }
            else
            {
                mirorObj = Instantiate(obj.gameObject, mirrorObjPos, obj.transform.rotation);
            }
            // prevents a chain reaction of a wrong spawning
            if (mirorObj.tag != "Spaceship")
            {
                Physics.IgnoreCollision(mirorObj.GetComponent<Collider>(), colToIgnore, true);
            }
        }
    
    }
}
