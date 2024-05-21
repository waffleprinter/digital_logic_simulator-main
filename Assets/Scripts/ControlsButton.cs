using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsButton : MonoBehaviour {
    public void RemoveFPS() {
        Settings.fpsOn = !Settings.fpsOn;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            SceneManager.LoadScene("Controls");
        }
    }
}
