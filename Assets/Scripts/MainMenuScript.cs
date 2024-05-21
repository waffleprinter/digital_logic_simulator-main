using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("Level 1");
    }

    public void Options() {
        SceneManager.LoadScene("Options");
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame() {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void Level1() {
        SceneManager.LoadScene("Level 1");
    }

    public void Level2() {
        SceneManager.LoadScene("Level 2");
    }

    public void Level3() {
        SceneManager.LoadScene("Level 3");
    }
}
