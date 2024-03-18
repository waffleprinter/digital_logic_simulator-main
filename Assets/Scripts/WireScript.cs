using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireScript : MonoBehaviour {
    public GameObject output;
    public GameObject input;

    private NodeLogicScript nodeLogicScript;
    private WireManagerScript wireManagerScript;

    private LineRenderer wireRenderer;
    
    private void Start() {
        nodeLogicScript = GetParentNode(output).GetComponent<NodeLogicScript>();
        wireManagerScript = GameObject.Find("Wire Manager").GetComponent<WireManagerScript>();

        wireRenderer = GetComponent<LineRenderer>();
        wireRenderer.positionCount = 2;
    }

    private void Update() {
        if (output == null || input == null) {
            wireManagerScript.wires.Remove(gameObject);
            Destroy(gameObject);
            return;
        }

        wireRenderer.SetPosition(0, output.transform.position);
        wireRenderer.SetPosition(1, input.transform.position);

        wireRenderer.material.color = nodeLogicScript.GetOutput() ? Color.green: Color.red;
    }

    private GameObject GetParentNode(GameObject child) {
        while (child.GetComponent<NodeLogicScript>() == null && child.transform.parent != null) {
            child = child.transform.parent.gameObject;
        }

        return child;
    }
}
