using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraZoomScript : MonoBehaviour
{
    private Camera cam;

    private float zoom;
    private float zoomMultiplier = 4f;
    private float minZoom = 1f;
    private float maxZoom = 15f;
    private float velocity = 0f;
    private float smoothTime = 0.05f;

    private void Start() {
        cam = GetComponent<Camera>();
        zoom = cam.orthographicSize;
    }

    private void Update() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        zoom -= zoomMultiplier * scroll;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);

        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
    }
}
