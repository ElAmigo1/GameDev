using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    public void Yes()
    {
        string lastScene = PlayerPrefs.GetString("LastScene", "level1");
        SceneManager.LoadSceneAsync(lastScene);
    }
    public void No()
    {
        SceneManager.LoadSceneAsync("start");
    }
}
