using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("level1");
    }
    public void ShowOptions()
    {
        SceneManager.LoadSceneAsync("setting");
    }
    public void BackToStart()
    {
        SceneManager.LoadSceneAsync("start");
    }
}
