using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBoundsSetter : MonoBehaviour
{
    [SerializeField] BoxCollider topBorder = null;
    [SerializeField] BoxCollider bottomBorder = null;
    [SerializeField] BoxCollider rightBorder = null;
    [SerializeField] BoxCollider leftBorder = null;
    [SerializeField] BoxCollider topRightBorder = null;
    [SerializeField] BoxCollider topLeftBorder = null;
    [SerializeField] BoxCollider bottomRightBorder = null;
    [SerializeField] BoxCollider bottomLeftBorder = null;
    [SerializeField] BoxCollider worldBounds = null;
    [SerializeField] RectTransform rightPanelReactTran = null;
    [SerializeField] RectTransform leftPanelReactTran = null;

    private void Start()
    {
        InitBordersRange();
    }

    private void InitBordersRange()
    {
        (float, float) screenDimentions = CalcTool.GetScreenSize();
        float frustumWidth = screenDimentions.Item1;
        float frustumHeight = screenDimentions.Item2;
        float x = 2f * Camera.main.orthographicSize * Camera.main.aspect;
        Vector3 rightUIDimentionsRatio = leftPanelReactTran.anchorMax - leftPanelReactTran.anchorMin;
        Vector3 leftUIDimentionsRatio = rightPanelReactTran.anchorMax - rightPanelReactTran.anchorMin;
        Vector3 rightUIDimentions = new Vector3(frustumWidth * rightUIDimentionsRatio.x, frustumHeight * rightUIDimentionsRatio.y, 1);
        Vector3 leftUIDimentions = new Vector3(frustumWidth * leftUIDimentionsRatio.x, frustumHeight * leftUIDimentionsRatio.y, 1);
        Vector3 camPos = Camera.main.transform.position;
        // setting borders widht
        rightBorder.size = rightUIDimentions;
        leftBorder.size = leftUIDimentions;
        topBorder.size = new Vector3(frustumWidth - rightUIDimentions.x - leftUIDimentions.x, 1, 1);
        bottomBorder.size = new Vector3(frustumWidth - rightUIDimentions.x - leftUIDimentions.x, 1, 1);

        topRightBorder.size = new Vector3(rightBorder.size.x, topBorder.size.y, 0);
        topLeftBorder.size = new Vector3(leftBorder.size.x, topBorder.size.y, 0);
        bottomRightBorder.size = new Vector3(rightBorder.size.x, bottomBorder.size.y, 0);
        bottomLeftBorder.size = new Vector3(leftBorder.size.x, bottomBorder.size.y, 0);
        // Setting borders pos
        topBorder.transform.position = new Vector3(0, camPos.y + frustumHeight / 2 + topBorder.size.y / 2, 0);
        bottomBorder.transform.position = new Vector3(0, camPos.y - (frustumHeight / 2 + bottomBorder.size.y / 2), 0);
        rightBorder.transform.position = new Vector3(camPos.x + frustumWidth / 2 - rightBorder.size.x / 2, 0, 0);
        leftBorder.transform.position = new Vector3(camPos.x - (frustumWidth / 2 - leftBorder.size.x / 2), 0, 0);

        topRightBorder.transform.position = new Vector3(rightBorder.transform.position.x, topBorder.transform.position.y, 0);
        topLeftBorder.transform.position = new Vector3(leftBorder.transform.position.x, topBorder.transform.position.y, 0);
        bottomRightBorder.transform.position = new Vector3(rightBorder.transform.position.x, bottomBorder.transform.position.y, 0);
        bottomLeftBorder.transform.position = new Vector3(leftBorder.transform.position.x, bottomBorder.transform.position.y, 0);

        // setting game playing area
        worldBounds.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        worldBounds.size = new Vector3(frustumWidth /*- _spaceshipSize*/ - rightUIDimentions.x - leftUIDimentions.x, frustumHeight /*- _spaceshipSize*/, 1f);
    }

}
