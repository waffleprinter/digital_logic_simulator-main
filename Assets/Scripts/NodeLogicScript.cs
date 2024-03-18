using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum NodeType {
    SWITCH,
    LED,
    NOT,
    AND,
    OR,
    NAND,
    NOR,
    XOR,
    XNOR,
    CLOCK
}

public class NodeLogicScript : MonoBehaviour {
    private readonly Dictionary<NodeType, int> numberOfInputNodes = new Dictionary<NodeType, int>() {
        { NodeType.SWITCH, 0 },
        { NodeType.LED, 1 },
        { NodeType.NOT, 1 },
        { NodeType.AND, 2 },
        { NodeType.OR, 2 },
        { NodeType.NAND, 2 },
        { NodeType.NOR, 2 },
        { NodeType.XOR, 2 },
        { NodeType.XNOR, 2 },
        { NodeType.CLOCK, 0 }
    };

    [SerializeField] private NodeType nodeType;

    public List<List<GameObject>> inputNodes = new List<List<GameObject>>();
    [SerializeField] private bool output; // [SERIALIZEFIELD] TO SEE IN INSPECTOR

    private float clockSpeed = 1;

    private void Start() {
        for (int i = 0; i < numberOfInputNodes[nodeType]; i++) {
            inputNodes.Add(new List<GameObject>());
        }

        if (nodeType == NodeType.CLOCK) {
            InvokeRepeating("ToggleOutput", 0, clockSpeed);
        }
    }

    private void Update() {
        foreach (List<GameObject> inputNodeConnections in inputNodes) {
            for (int i = 0; i < inputNodeConnections.Count; i++) {
                if (inputNodeConnections[i] == null) {
                    inputNodeConnections.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    private void LateUpdate() {
        output = CalculateOutput();
    }

    public bool GetOutput() {
        return output;
    }

    public void ToggleOutput() {
        output = !output;
    }

    private List<bool> GetEvaluatedInputs() {
        List<bool> evaluatedInputs = new List<bool>();

        foreach (List<GameObject> inputNodeConnections in inputNodes) {
            bool signal = false;

            foreach (GameObject inputConnectionSignal in inputNodeConnections) {
                if (inputConnectionSignal == null) continue;
                
                if (inputConnectionSignal.GetComponent<NodeLogicScript>().GetOutput()) {
                    signal = true;
                    break;
                }
            }

            evaluatedInputs.Add(signal);
        }

        return evaluatedInputs;
    }

    private bool CalculateOutput() {
        List<bool> evaluatedInputs = GetEvaluatedInputs();

        switch (nodeType) {
            case NodeType.SWITCH:
                return output;

            case NodeType.LED:
                return evaluatedInputs[0];

            case NodeType.NOT:
                return !evaluatedInputs[0];

            case NodeType.AND:
                return evaluatedInputs[0] && evaluatedInputs[1];

            case NodeType.OR:
                return evaluatedInputs[0] || evaluatedInputs[1];

            case NodeType.NAND:
                return !(evaluatedInputs[0] && evaluatedInputs[1]);

            case NodeType.NOR:
                return !(evaluatedInputs[0] || evaluatedInputs[1]);

            case NodeType.XOR:
                return evaluatedInputs[0] ^ evaluatedInputs[1];

            case NodeType.XNOR:
                return !(evaluatedInputs[0] ^ evaluatedInputs[1]);

            case NodeType.CLOCK:
                return output;
        }

        return false;
    }
}
