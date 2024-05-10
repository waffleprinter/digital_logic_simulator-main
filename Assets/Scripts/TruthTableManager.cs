using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class TruthTableManager : MonoBehaviour
{
    public GameObject selectingText;

    private bool selecting = false;

    public List<GameObject> switches = new List<GameObject>();
    public List<GameObject> leds = new List<GameObject>();

    private GameObject gateHit;
    private NodeLogicScript swScript;

    private void Update() {
        selectingText.gameObject.SetActive(selecting);

        if (Input.GetKeyDown(KeyCode.Tab)) selecting = !selecting;

        if (!selecting) return;

        if (Input.GetMouseButtonDown(0)) {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, float.PositiveInfinity, LayerMask.GetMask("Gate"));
            
            if (hit.collider == null) return;

            gateHit = hit.collider.gameObject;

            if (gateHit.CompareTag("Switch") && !switches.Contains(gateHit)) switches.Add(gateHit);
            else if (gateHit.CompareTag("LED") && !leds.Contains(gateHit)) leds.Add(gateHit);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            switches = new List<GameObject>();
            leds = new List<GameObject>();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            printTruthTable();
        }
    }

    public List<List<int>> GetTruthTable() {
        List<List<int>> truthTable = new List<List<int>>(); // [[in1, in2], [out1, out2], [in1, in2], [out1, out2]]

        ResetSwitches();

        var gates = GameObject.FindGameObjectsWithTag("Gate");
        var allLeds = GameObject.FindGameObjectsWithTag("LED");
        var permutations = Enumerable.Range(0, (int)Math.Pow(2, switches.Count)).Select(i => Convert.ToString(i, 2).PadLeft(switches.Count, '0'));

        foreach (var perm in permutations) {
            ResetSwitches();
            List<int> currentInputs = new List<int>();

            for (int i = 0; i < perm.Length; i++) {
                if (perm[i] == '1') {
                    currentInputs.Add(1);
                    switches[i].GetComponent<NodeLogicScript>().ToggleOutput();
                } else {
                    currentInputs.Add(0);
                }
            }

            truthTable.Add(currentInputs);

            foreach (var gate in gates) {
                gate.GetComponent<NodeLogicScript>().output = gate.GetComponent<NodeLogicScript>().CalculateOutput();
            }

            foreach (var led in allLeds) {
                led.GetComponent<NodeLogicScript>().output = led.GetComponent<NodeLogicScript>().CalculateOutput();
            }

            List<int> currentOutputs = new List<int>();

            for (int i = leds.Count - 1; i >= 0; i--) {
                if (leds[i].GetComponent<NodeLogicScript>().GetOutput()) currentOutputs.Add(1);
                else currentOutputs.Add(0);
            }

            truthTable.Add(currentOutputs);
        }

        return truthTable;
    }

    private void ResetSwitches() {
        foreach (GameObject sw in switches) {
            swScript = sw.GetComponent<NodeLogicScript>();
            if (swScript.GetOutput()) swScript.ToggleOutput();
        }
    }

    private void printTruthTable() {
        List<List<int>> truthTable = GetTruthTable();

        for (int i = 0; i < truthTable.Count; i += 2) {
            string inputs = "";
            string outputs = "";

            foreach (int value in truthTable[i]) {
                inputs += value.ToString();
            }

            foreach (int value in truthTable[i + 1]) {
                outputs += value.ToString();
            }

            Debug.Log(inputs + " " + outputs);
        }

        Debug.Log("");
    }
}
