using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WireManagerScript : MonoBehaviour {
    [SerializeField] private GameObject wirePrefab;
    
    public List<GameObject> wires = new List<GameObject>();

    public void CreateWire(GameObject output, GameObject input) {
        GameObject wire = Instantiate(wirePrefab);
        WireScript wireScript = wire.GetComponent<WireScript>();

        wireScript.output = output;
        wireScript.input = input;

        wires.Add(wire);
    }

    public void DestroyWire(GameObject output, GameObject input) {
        foreach (GameObject wire in wires) {
            if (wire == null) continue;
            
            WireScript wireScript = wire.GetComponent<WireScript>();

            if (wireScript.output == output && wireScript.input == input) {
                wires.Remove(wire);
                Destroy(wire);
                return;
            }
        }
    }
}


