using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private Transform dragging = null;
    private Vector3 offset;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, float.PositiveInfinity, LayerMask.GetMask("Gate"));

            if (hit) {
                dragging = hit.transform;
                offset = dragging.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        } else if (Input.GetMouseButtonUp(0)) {
            dragging = null;
        }

        if (dragging != null) {
            dragging.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }
}
