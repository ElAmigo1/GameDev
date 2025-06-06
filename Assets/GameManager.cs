using UnityEngine;
using UnityEngine.UI; // if using old UI Text
using TMPro;          // if using TextMeshPro (optional)

public class GameManager : MonoBehaviour
{
    public GameObject popupPanel;
    public TMP_Text popupText;
    public float popupDuration = 2f; // seconds

    public static GameManager instance;

    public int coinsCollected = 0;
    public int coinsNeeded = 3;

    public TMP_Text coinText; // or TMP_Text if using TextMeshPro
    public GameObject portalPrefab;
    public Transform portalSpawnLocation;

    private bool portalSpawned = false;

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (coinText == null)
        {
            Debug.LogError("CoinText wurde im Inspector nicht gesetzt!");
        }
        else
        {
            Debug.Log("CoinText wurde korrekt gefunden.");
        }

        UpdateUI();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CollectCoin();
        }
    }


    public void CollectCoin()
    {
        coinsCollected++;
        Debug.Log($"Collected a coin! Total: {coinsCollected}");

        UpdateUI();
      
        if (coinsCollected >= coinsNeeded && !portalSpawned)
        {
            ShowPopup($"Go to the Beginning of the End!"); // <= wird IMMER gezeigt!

            SpawnPortal();
        }
        else {
            ShowPopup($"Coin {coinsCollected} collected!"); // <= wird IMMER gezeigt!


        }
    }

    private void UpdateUI()
    {
        if (coinText != null)
        {
            Debug.Log($"UpdateUI called: {coinsCollected}/{coinsNeeded}");
            coinText.text = $"Coins: {coinsCollected}/{coinsNeeded}";
            Debug.Log($"coinText.text jetzt auf: {coinText.text} gesetzt");
        }
        else
        {
            Debug.LogError("Coin Text UI is not assigned in GameManager!");
        }
    }




    private void SpawnPortal()
    {
        if (portalPrefab != null && portalSpawnLocation != null)
        {
            Instantiate(portalPrefab, portalSpawnLocation.position, Quaternion.identity);
            Debug.Log("Portal spawned!");
            portalSpawned = true;
        }
        else
        {
            Debug.LogError("Portal Prefab or Spawn Location not assigned in GameManager!");
        }
    }
    public void ShowPopup(string message)
    {
        if (popupPanel != null && popupText != null)
        {
            popupText.text = message;
            popupPanel.SetActive(true);
            CancelInvoke(nameof(HidePopup)); // in case multiple coins are collected quickly
            Invoke(nameof(HidePopup), popupDuration);
        }
    }

    private void HidePopup()
    {
        popupPanel.SetActive(false);
    }

}
