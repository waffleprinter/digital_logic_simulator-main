using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class TruthTableManager : MonoBehaviour
{
    public List<GameObject> switches = new List<GameObject>();
    public List<GameObject> leds = new List<GameObject>();

    private bool selecting = false;

    private GameObject gateHit;
    private NodeLogicScript swScript;

    private void Update() {
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
            GetTruthTable();
        }
    }

    private void GetTruthTable() {
        ResetSwitches();

        var permutations = Enumerable.Range(0, (int)Math.Pow(2, switches.Count)).Select(i => Convert.ToString(i, 2).PadLeft(switches.Count, '0'));

        foreach (var perm in permutations) {
            ResetSwitches();

            for (int i = 0; i < perm.Length; i++) {
                if (perm[i] == '1') switches[i].GetComponent<NodeLogicScript>().ToggleOutput();
            }

            string ledOutput = "";

            for (int i = 0; i < leds.Count; i++) {
                if (leds[i].GetComponent<NodeLogicScript>().GetOutput()) ledOutput += '1';
                else ledOutput += '0';                
            }

            Debug.Log(perm);
            Debug.Log(ledOutput);
            Debug.Log("");
        }
    }

    private void ResetSwitches() {
        foreach (GameObject sw in switches) {
            swScript = sw.GetComponent<NodeLogicScript>();
            if (swScript.GetOutput()) swScript.ToggleOutput();
        }
    }
}
