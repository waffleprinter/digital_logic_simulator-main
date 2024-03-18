using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ConnectionManager : MonoBehaviour {
    private GameObject firstClickedObject = null;
    private GameObject secondClickedObject = null;

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, float.PositiveInfinity, LayerMask.GetMask("Input Output"));

            if (hit.collider != null) {
                GameObject clickedObject = hit.collider.gameObject;

                if (firstClickedObject == null) {
                    firstClickedObject = clickedObject;
                } else {
                    secondClickedObject = clickedObject;
                }

                if (firstClickedObject != null && secondClickedObject != null) { 
                    if (firstClickedObject.tag == secondClickedObject.tag) {
                        ResetClickedObjects();
                        return;
                    }

                    if (firstClickedObject.tag == "Input") {
                        ToggleConnection(secondClickedObject, firstClickedObject, GetNodeIndex(firstClickedObject));
                    } else if (firstClickedObject.tag == "Output") {
                        ToggleConnection(firstClickedObject, secondClickedObject, GetNodeIndex(secondClickedObject));
                    }
                }
            }
        }

        if (firstClickedObject != null && secondClickedObject != null) {
            ResetClickedObjects();
        }
    }

    private GameObject GetParentNode(GameObject child) {
        while (child.GetComponent<NodeLogicScript>() == null && child.transform.parent != null) {
            child = child.transform.parent.gameObject;
        }

        return child;
    }

    private void ToggleConnection(GameObject output, GameObject input, int inputNodeIndex) {
        GameObject outputtingNode = GetParentNode(output);
        GameObject inputNode = GetParentNode(input);

        List<GameObject> receivingInput = inputNode.GetComponent<NodeLogicScript>().inputNodes[inputNodeIndex];

        WireManagerScript wireManagerScript = GameObject.Find("Wire Manager").GetComponent<WireManagerScript>();

        if (receivingInput.Contains(outputtingNode)) {
            receivingInput.Remove(outputtingNode);
            wireManagerScript.DestroyWire(output, input);
        } else {
            receivingInput.Add(outputtingNode);
            wireManagerScript.CreateWire(output, input);
        }
    }

    private int GetNodeIndex(GameObject obj) {
        string objectName = obj.name;

        string pattern = "[^0-9]";
        string numberString = Regex.Replace(objectName, pattern, "");

        int.TryParse(numberString, out int number);

        return number - 1;
    }

    private void ResetClickedObjects() {
        firstClickedObject = null;
        secondClickedObject = null;
    }
}
