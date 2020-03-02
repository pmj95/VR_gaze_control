using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadGazeVisualizer : MonoBehaviour
{
    public Transform gazeOrigin;

    [Header("Settings")]
    [Range(0f, 1f)]
    public float confidenceThreshold = 0.6f;
    public bool binocularOnly = true;

    [Header("Projected Visualization")]
    public Transform projectionMarker;

    Vector3 localGazeDirection;
    float gazeDistance;
    bool isGazing = false;

    bool errorAngleBasedMarkerRadius = true;
    float angleErrorEstimate = 2f;

    Vector3 origMarkerScale;
    MeshRenderer targetRenderer;
    float minAlpha = 0.2f;
    float maxAlpha = 0.8f;

    float lastConfidence;

    void OnEnable()
    {
        if (projectionMarker == null)
        {
            Debug.LogWarning("Marker reference missing.");
            enabled = false;
            return;
        }

        if (gazeOrigin == null)
        {
            Debug.LogWarning("Required components missing.");
            enabled = false;
            return;
        }

        StartVisualizing();
    }

    void OnDisable()
    {

        StopVisualizing();
    }

    void Update()
    {
        if (!isGazing)
        {
            return;
        }

        VisualizeConfidence();

        ShowProjected();
    }

    public void StartVisualizing()
    {
        if (!enabled)
        {
            Debug.LogWarning("Component not enabled.");
            return;
        }

        if (isGazing)
        {
            Debug.Log("Already gazing!");
            return;
        }

        Debug.Log("Start Visualizing Gaze");

        projectionMarker.gameObject.SetActive(true);
        isGazing = true;
    }

    public void StopVisualizing()
    {
        if (!isGazing || !enabled)
        {
            Debug.Log("Nothing to stop.");
            return;
        }

        if (projectionMarker != null)
        {
            projectionMarker.gameObject.SetActive(false);
        }

        isGazing = false;
    }

    void ReceiveGaze()
    {
        if (binocularOnly)
        {
            return;
        }
    }

    void VisualizeConfidence()
    {
        if (targetRenderer != null)
        {
            Color c = targetRenderer.material.color;
            c.a = MapConfidence(lastConfidence);
            targetRenderer.material.color = c;
        }
    }

    void ShowProjected()
    {
        Vector3 origin = gazeOrigin.position;
        Vector3 direction = gazeOrigin.TransformDirection(this.transform.forward);

        if (Physics.Raycast(origin, direction, out RaycastHit hit))
        {
            Debug.DrawRay(origin, direction * hit.distance, Color.yellow);

            projectionMarker.position = hit.point;
        }
        else
        {
            Debug.DrawRay(origin, direction * 10, Color.white);
        }
    }

    Vector3 GetErrorAngleBasedScale(Vector3 origScale, float distance, float errorAngle)
    {
        Vector3 scale = origScale;
        float scaleXY = distance * Mathf.Tan(Mathf.Deg2Rad * angleErrorEstimate) * 2;
        scale.x = scaleXY;
        scale.y = scaleXY;
        return scale;
    }

    float MapConfidence(float confidence)
    {
        return Mathf.Lerp(minAlpha, maxAlpha, confidence);
    }
}