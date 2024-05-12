using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TruthTableUIGenerator : MonoBehaviour
{
    private List<List<int>> targetTruthTable = new List<List<int>> {  // AND GATE TRUTH TABLE
        new List<int> {0, 0},
        new List<int> {0},
        new List<int> {0, 1},
        new List<int> {0},
        new List<int> {1, 0},
        new List<int> {0},
        new List<int> {1, 1},
        new List<int> {1},
    };

    [SerializeField] private GameObject truthTableBackgroundPrefab;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private TextMeshProUGUI cellTextPrefab;

    private int numberOfInputs;
    private int numberOfOutputs;

    private int numberOfRows;
    private double numberOfColumns;

    private void Start() {
        numberOfInputs = targetTruthTable[0].Count;
        numberOfOutputs = targetTruthTable[1].Count;

        numberOfRows = numberOfInputs + numberOfOutputs;
        numberOfColumns = Math.Pow(2, Math.Max(numberOfInputs, numberOfOutputs)) + 1;

        Vector2 cellSize = truthTableBackgroundPrefab.GetComponent<GridLayoutGroup>().cellSize;

        GameObject truthTableBackground = Instantiate(truthTableBackgroundPrefab);
        truthTableBackground.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);

        truthTableBackground.GetComponent<RectTransform>().sizeDelta = new Vector2 (
            numberOfRows * (cellSize.x + 10) + 10,
            (float)(numberOfColumns * (cellSize.y + 10) + 10)
        );

        truthTableBackground.transform.position = new Vector3 (
            1920 - (truthTableBackground.GetComponent<RectTransform>().sizeDelta.x + 20) / 2,
            (truthTableBackground.GetComponent<RectTransform>().sizeDelta.y + 20) / 2,
            0
        );

        int sublist = 0;
        int index = 0;

        for (int i = 0; i < numberOfRows * numberOfColumns; i++) {
            GameObject cell = Instantiate(cellPrefab);
            cell.transform.SetParent(truthTableBackground.transform);

            TextMeshProUGUI cellText = Instantiate(cellTextPrefab);
            cellText.transform.SetParent(cell.transform);

            if (i < numberOfInputs) { cellText.text = "I" + (i + 1).ToString(); } 
            else if (i < numberOfRows) { cellText.text = "O" + (i - numberOfInputs + 1).ToString(); } 
            else {
                cellText.text = targetTruthTable[sublist][index].ToString();

                index++;
                if (index == targetTruthTable[sublist].Count) {
                    sublist++;
                    index = 0;
                }
            }
        }
    }
 }
