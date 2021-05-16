using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSubScroller : MonoBehaviour
{
    public float backgroundImageWidth;

    private Transform[] horizontalLayers; // horizontal layers collect layers sequentially from top to bottomn and indexes from intervals.
    private Transform cameraTransform;

    // Mostly constant
    private float parallaxSpeed;
    private float camerViewSize;
    private int horizontalLayerDepth;
    private int verticalLayerDepth;

    // Index tracker
    private int rightIndex;
    private int leftIndex;
    private int arrayIndex = 0;

    private float deltaX;
    private float lastCameraHorPos;
    private Vector2 horizontalLayerPos;

    public void InitialiseScroller(Transform[] verticalLayers, float cameraSize, float parallaxSpeed)
    {
        this.camerViewSize = cameraSize;
        this.parallaxSpeed = parallaxSpeed;
        cameraTransform = Camera.main.transform;
        lastCameraHorPos = cameraTransform.position.x;

        horizontalLayers = new Transform[verticalLayers[0].childCount * verticalLayers.Length];
        for (int i = 0; i < verticalLayers.Length; i++)
        {
            for (int j = 0; j < verticalLayers[i].childCount; j++)
            {
                horizontalLayers[i * 3 + j] = verticalLayers[i].GetChild(j);
            }
        }

        horizontalLayerDepth = verticalLayers[0].childCount;
        verticalLayerDepth = verticalLayers.Length;
        leftIndex = 0;
        rightIndex = horizontalLayerDepth - 1;
    }

    public void RunScrollHorizontally()
    {
        ScrollHorizontally();
    }

    /// <summary>
    /// Called to handle vertical scrolling of the background
    /// </summary>
    private void ScrollHorizontally()
    {
        deltaX = cameraTransform.position.x - lastCameraHorPos;
        transform.position += Vector3.right * (deltaX * parallaxSpeed);
        lastCameraHorPos = cameraTransform.position.x;

        if (cameraTransform.position.x < (horizontalLayers[leftIndex].position.x + 3))
        {
            RepositionLayerLeft();
        }


        if (cameraTransform.position.x > horizontalLayers[rightIndex].position.x)
        {
            RepositionLayerRight();
        }
    }

    /// <summary>
    /// Called to shift the layer down to the bottom index
    /// </summary>
    private void RepositionLayerLeft()
    {
        for (int i = 0; i < verticalLayerDepth; i++)
        {
            arrayIndex = i * horizontalLayerDepth + rightIndex;
            horizontalLayerPos = new Vector2(horizontalLayers[leftIndex].position.x - backgroundImageWidth, horizontalLayers[arrayIndex].position.y);
            horizontalLayers[i * horizontalLayerDepth + rightIndex].position = horizontalLayerPos;
        }

        leftIndex = rightIndex;
        rightIndex--;

        if (rightIndex < 0)
            rightIndex = horizontalLayerDepth - 1;
    }

    /// <summary>
    /// Called to shift the layer  up to the top index
    /// </summary>
    private void RepositionLayerRight()
    {
        for (int i = 0; i < verticalLayerDepth; i++)
        {
            arrayIndex = i * horizontalLayerDepth + leftIndex;
            horizontalLayerPos = new Vector2(horizontalLayers[rightIndex].position.x + backgroundImageWidth, horizontalLayers[arrayIndex].position.y);
            horizontalLayers[i * horizontalLayerDepth + leftIndex].position = horizontalLayerPos;
        }

        rightIndex = leftIndex;
        leftIndex++;

        if (leftIndex == horizontalLayerDepth)
            leftIndex = 0;
    }
}
