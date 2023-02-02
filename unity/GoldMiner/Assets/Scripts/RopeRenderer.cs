using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private Transform startPosition;
    [SerializeField] private float lineWidth;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.enabled = false;
    }

    public void RenderLine(Vector3 endPosition, bool enableRender)
    {
        if (enableRender)
        {
            if (!lineRenderer.enabled)
                lineRenderer.enabled = true;

            lineRenderer.positionCount = 2;
        }
        else
        {
            lineRenderer.positionCount = 0;

            if (lineRenderer.enabled)
                lineRenderer.enabled = false;
        }

        if (lineRenderer.enabled)
        {
            Vector3 currentStartPosition = startPosition.position;
            currentStartPosition.z = -10f;
            startPosition.position = currentStartPosition;

            Vector3 currentEndPosition = endPosition;
            currentEndPosition.z = 0f;
            endPosition = currentEndPosition;

            lineRenderer.SetPosition(0, startPosition.position);
            lineRenderer.SetPosition(1, endPosition);
        }
    }
}