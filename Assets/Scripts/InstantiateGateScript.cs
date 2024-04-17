using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGateScript : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    private GameObject truthTableManager;
    private GameObject instantiatedGate;
    private bool instantiationStarted;

    private void Start() {
        truthTableManager = GameObject.Find("Truth Table Manager");
    }

    private void Update() {
        if (!instantiationStarted) return;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (instantiatedGate == null) {
            instantiatedGate = Instantiate(gate, mousePosition, Quaternion.identity);

            if (gate.name == "SWITCH") truthTableManager.GetComponent<TruthTableManager>().switches.Add(instantiatedGate);
            else if (gate.name == "LED") truthTableManager.GetComponent<TruthTableManager>().leds.Add(instantiatedGate);
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
