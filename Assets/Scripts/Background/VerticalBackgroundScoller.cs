using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalBackgroundScoller : MonoBehaviour
{
    public float backgroundImageHeight;
    //public float horizontalLength;
    public float cameraViewSize;

    [Space]
    public float paralaxSpeed;

    private Transform[] verticalLayers;
     

    public Transform cameraTransform;

    private int bottomIndex;
    private int topIndex;

    private int verticalLayerDepth;

    private float deltaY;
    //private float deltaX;

    private float lastCameraVertPos;
    private Vector2 verticalLayerPos;
   // private Vector2 horizontalLayerPos;

    /// <summary>
    /// 
    /// </summary>
    public void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraVertPos = cameraTransform.position.y;
        cameraViewSize = Camera.main.orthographicSize;

        verticalLayers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            verticalLayers[i] = transform.GetChild(i);
        }

        verticalLayerDepth = verticalLayers.Length;
        bottomIndex = 0;
        topIndex = verticalLayers.Length - 1;
    }

    private void FixedUpdate()
    {
        ScrollVertically();
    }

    /// <summary>
    /// Controls the sub horizontal scrollers
    /// </summary>
    private void RunSubHorizontaqlScoller()
    {

    }

    /// <summary>
    /// Called to handle vertical scrolling of the background
    /// </summary>
    private void ScrollVertically()
    {
        deltaY = cameraTransform.position.y - lastCameraVertPos;
        transform.position += Vector3.up * (deltaY * paralaxSpeed);
        lastCameraVertPos = cameraTransform.position.y;


        if (cameraTransform.position.y < (verticalLayers[bottomIndex].position.y + 3))
        {
            RepositionLayerDown();
        }


        if (cameraTransform.position.y > verticalLayers[topIndex].position.y)
        {
            RepositionLayerUp();
        }
    }

    /// <summary>
    /// Called to shift the layer down to the bottom index
    /// </summary>
    private void RepositionLayerDown()
    {
        verticalLayerPos = new Vector2(verticalLayers[bottomIndex].position.x, verticalLayers[bottomIndex].position.y - backgroundImageHeight);
        verticalLayers[topIndex].position = verticalLayerPos;
        bottomIndex = topIndex;
        topIndex--;

        if (topIndex < 0)
            topIndex = verticalLayerDepth - 1;
    }

    /// <summary>
    /// Called to shift the layer  up to the top index
    /// </summary>
    private void RepositionLayerUp()
    {
        verticalLayerPos = new Vector2(verticalLayers[topIndex].position.x, verticalLayers[topIndex].position.y + backgroundImageHeight);
        verticalLayers[bottomIndex].position = verticalLayerPos;
        topIndex = bottomIndex;
        bottomIndex++;

        if (bottomIndex == verticalLayerDepth)
            bottomIndex = 0;
    }
}
