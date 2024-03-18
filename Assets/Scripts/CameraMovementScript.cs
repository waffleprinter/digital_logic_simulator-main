using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    private Camera cam;

    private Vector3 origin;
    private Vector3 offset;

    private bool dragging;

    private void Start() {
        cam = GetComponent<Camera>();
    }

    private void Update() {
        if (Input.GetMouseButton(2)) {
            offset = cam.ScreenToWorldPoint(Input.mousePosition) - cam.transform.position;

            if (!dragging) {
                dragging = true;
                origin = cam.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        else {
            dragging = false;
        }

        if (dragging) {
            cam.transform.position = origin - offset;
        }
    }
    
}
