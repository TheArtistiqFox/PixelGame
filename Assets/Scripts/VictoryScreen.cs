using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public void ReplayGame()
    {
        SceneManager.LoadScene("FloorScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
