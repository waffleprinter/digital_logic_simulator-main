using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteNodeScript : MonoBehaviour
{
    private bool willDelete = false;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace)) {
            willDelete = true;
        }

        if (!willDelete) return;

        if (Input.GetMouseButtonDown(0)) {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, float.PositiveInfinity, LayerMask.GetMask("Gate"));

            if (hit.collider == null) return;

            Destroy(hit.collider.gameObject);
            
            willDelete = false;
        }
    }
}
