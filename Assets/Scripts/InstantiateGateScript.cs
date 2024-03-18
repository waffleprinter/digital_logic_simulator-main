using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGateScript : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    private GameObject instantiatedGate;
    private bool instantiationStarted;

    private void Update() {
        if (!instantiationStarted) return;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (instantiatedGate == null) {
            instantiatedGate = Instantiate(gate, mousePosition, Quaternion.identity);
        }

        if (Input.GetMouseButtonDown(0)) {
            instantiatedGate = null;
            instantiationStarted = false;
        } else {
            instantiatedGate.transform.position = mousePosition;
        }
    }

    public void StartInstantiation() {
        instantiationStarted = true;
    }
}
