using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Created using Brackey's video and what was discussed about menu scene management in a previous google meeting

    public void PlayGame()
    {
        SceneManager.LoadScene("FloorScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
