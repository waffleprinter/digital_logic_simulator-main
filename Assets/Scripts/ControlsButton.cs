using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsButton : MonoBehaviour {
    private void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            SceneManager.LoadScene("Controls");
        }
    }
}
