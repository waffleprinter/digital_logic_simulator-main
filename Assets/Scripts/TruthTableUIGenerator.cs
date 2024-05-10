using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruthTableUIGenerator : MonoBehaviour
{
    private int numberOfInputs = 2;
    private int numberOfOutputs = 2;

    [SerializeField] private GameObject truthTableBackgroundPrefab;
    [SerializeField] private GameObject cellPrefab;

    private void Start() {
        Vector2 cellSize = truthTableBackgroundPrefab.GetComponent<GridLayoutGroup>().cellSize;

        GameObject truthTableBackground = Instantiate(truthTableBackgroundPrefab) as GameObject;
        truthTableBackground.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        truthTableBackground.transform.position = new Vector3();
    }
 }
