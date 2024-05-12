using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TruthTableUIGenerator : MonoBehaviour {
    [SerializeField] private GameObject truthTableBackgroundPrefab;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private TextMeshProUGUI cellTextPrefab;

    private Scene scene;

    List<List<int>> targetTruthTable;
    GameObject playerTruthTable;

    private bool truthTablesVisible = false;
    

    private void Start() {
        scene = SceneManager.GetActiveScene();
        targetTruthTable = GetTargetTruthTable(scene);

        DisplayTruthTable(targetTruthTable, 1920, 0, false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            truthTablesVisible = !truthTablesVisible;

            foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>(includeInactive:true)) {
                if (obj.CompareTag("Truth Table")) {
                    obj.SetActive(truthTablesVisible);
                }
            }
        }
    }

    public void DisplayTruthTable(List<List<int>> truthTable, int x, int y, bool isPlayerTable) {
        if (playerTruthTable) {
            Destroy(playerTruthTable);
        }

        int numberOfInputs = truthTable[0].Count;
        int numberOfOutputs = truthTable[1].Count;

        int numberOfRows = numberOfInputs + numberOfOutputs;
        double numberOfColumns = Math.Pow(2, Math.Max(numberOfInputs, numberOfOutputs)) + 1;

        Vector2 cellSize = truthTableBackgroundPrefab.GetComponent<GridLayoutGroup>().cellSize;

        GameObject truthTableBackground = Instantiate(truthTableBackgroundPrefab);
        if (isPlayerTable) playerTruthTable = truthTableBackground;
        truthTableBackground.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);

        truthTableBackground.GetComponent<RectTransform>().sizeDelta = new Vector2 (
            numberOfRows * (cellSize.x + 10) + 10,
            (float)(numberOfColumns * (cellSize.y + 10) + 10)
        );

        truthTableBackground.transform.position = new Vector3 (
            isPlayerTable ? (truthTableBackground.GetComponent<RectTransform>().sizeDelta.x + 60) / 2 + 160: x - (truthTableBackground.GetComponent<RectTransform>().sizeDelta.x + 60) / 2,
            y + (truthTableBackground.GetComponent<RectTransform>().sizeDelta.y + 80) / 2,
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
                cellText.text = truthTable[sublist][index].ToString();

                index++;
                if (index == truthTable[sublist].Count) {
                    sublist++;
                    index = 0;
                }
            }
        }
    }

    public List<List<int>> GetTargetTruthTable(Scene scene) {
        switch (scene.name) {
            case "Level 1": 
                targetTruthTable = new List<List<int>> {  // AND GATE TRUTH TABLE
                new List<int> {0, 0}, new List<int> {0},
                new List<int> {0, 1}, new List<int> {0},
                new List<int> {1, 0}, new List<int> {0},
                new List<int> {1, 1}, new List<int> {1},
                };

                break;
            
            case "Level 2":
                targetTruthTable = new List<List<int>> {  // HALF ADDER TRUTH TABLE
                new List<int> {0, 0}, new List<int> {0, 0},
                new List<int> {0, 1}, new List<int> {0, 1},
                new List<int> {1, 0}, new List<int> {0, 1},
                new List<int> {1, 1}, new List<int> {1, 0},
                };

                break;

            case "Level 3":
                targetTruthTable = new List<List<int>> {  // FULL ADDER TRUTH TABLE
                new List<int> {0, 0, 0}, new List<int> {0, 0},
                new List<int> {0, 0, 1}, new List<int> {0, 1},
                new List<int> {0, 1, 0}, new List<int> {0, 1},
                new List<int> {0, 1, 1}, new List<int> {1, 0},
                new List<int> {1, 0, 0}, new List<int> {0, 1},
                new List<int> {1, 0, 1}, new List<int> {1, 0},
                new List<int> {1, 1, 0}, new List<int> {1, 0},
                new List<int> {1, 1, 1}, new List<int> {1, 1},
                };

                break;
        }

        return targetTruthTable;
    }
 }
