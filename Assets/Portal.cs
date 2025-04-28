using UnityEngine;
using UnityEngine.SceneManagement; // Needed for loading scenes
using System.Collections;


public class Portal : MonoBehaviour
{
    public string sceneToLoad = "Level2"; // Name of the scene to load

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Make sure your player has the "Player" tag
        {
            Debug.Log("Player entered portal! Loading next level...");
            SceneManager.LoadScene(sceneToLoad); // Load the specified scene
        }
    }

    //private IEnumerator LoadSceneAfterDelay(float delay)
    //{
    //    yield return new WaitForSeconds(delay); // Wait for the specified delay
    //}
}
