using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputColor : MonoBehaviour
{
    private NodeLogicScript nodeLogicScript;
    private GameObject body;

    private void Start() {
        nodeLogicScript = GetComponent<NodeLogicScript>();
        body = transform.GetChild(0).gameObject;
    }

    private void Update() {
        body.GetComponent<SpriteRenderer>().material.color = nodeLogicScript.GetOutput() ? Color.green: Color.red;
    }
}
