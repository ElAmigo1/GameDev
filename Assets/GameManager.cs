using UnityEngine;
using TMPro; // für TextMeshPro

public class GameManager : MonoBehaviour
{
    public GameObject popupPanel;
    public TMP_Text popupText;
    public float popupDuration = 2f; // Sekunden

    public static GameManager instance;

    public int coinsCollected = 0;
    public int coinsNeeded = 3;

    public TMP_Text coinText; // TextMeshPro UI Text
    public GameObject doorPrefab;           // Tür-Prefab
    public Transform doorSpawnLocation;     // Tür-Spawnpunkt

    private bool doorSpawned = false;

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Optional: Falls du den Manager über Szenen hinweg behalten willst
        }
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
            UpdateUI();
        }

        if (popupPanel != null)
            popupPanel.SetActive(false);  // Popup zu Beginn ausblenden

        if (doorPrefab == null || doorSpawnLocation == null)
            Debug.LogWarning("Door Prefab oder Door Spawn Location ist nicht zugewiesen!");
    }

    void Update()
    {
        // Zum Test: Münze sammeln mit Taste C (kann entfernt werden, wenn du echtes Sammeln hast)
        if (Input.GetKeyDown(KeyCode.C))
        {
            CollectCoin();
        }
    }

    public void CollectCoin()
    {
        coinsCollected++;
        Debug.Log($"Collected a Strawberry! Total: {coinsCollected}");

        UpdateUI();
        ShowPopup($"Strawberry {coinsCollected} collected!");

        if (coinsCollected >= coinsNeeded && !doorSpawned)
        {
            SpawnDoor();
        }
    }

    private void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = $"Coins: {coinsCollected}/{coinsNeeded}";
            Debug.Log($"coinText.text jetzt auf: {coinText.text} gesetzt");
        }
    }

    private void SpawnDoor()
    {
        if (doorPrefab != null && doorSpawnLocation != null)
        {
            Instantiate(doorPrefab, doorSpawnLocation.position, Quaternion.identity);
            Debug.Log("Door spawned!");
            doorSpawned = true;
        }
        else
        {
            Debug.LogError("Door Prefab oder Door Spawn Location ist nicht gesetzt!");
        }
    }

    public void ShowPopup(string message)
    {
        if (popupPanel != null && popupText != null)
        {
            popupText.text = message;
            popupPanel.SetActive(true);
            CancelInvoke(nameof(HidePopup)); // Falls mehrere Münzen schnell gesammelt werden
            Invoke(nameof(HidePopup), popupDuration);
        }
        else
        {
            Debug.LogWarning("PopupPanel oder PopupText nicht zugewiesen!");
        }
    }

    private void HidePopup()
    {
        if (popupPanel != null)
            popupPanel.SetActive(false);
    }
}
