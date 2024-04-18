using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteNodeScript : MonoBehaviour
{
    private bool willDelete = false;
    private GameObject truthTableManager;

    private void Start() {
        truthTableManager = GameObject.Find("Truth Table Manager");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace)) {
            willDelete = true;
        }

        if (!willDelete) return;

        if (Input.GetMouseButtonDown(0)) {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, float.PositiveInfinity, LayerMask.GetMask("Gate"));

            if (hit.collider == null) return;

            if (hit.collider.gameObject.CompareTag("Switch")) truthTableManager.GetComponent<TruthTableManager>().switches.Remove(hit.collider.gameObject);
            else if (hit.collider.gameObject.CompareTag("LED")) truthTableManager.GetComponent<TruthTableManager>().leds.Remove(hit.collider.gameObject);
            Destroy(hit.collider.gameObject);
            
            willDelete = false;
        }
    }
}
