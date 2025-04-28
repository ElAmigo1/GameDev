using EthanTheHero;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostRecorder : MonoBehaviour
{
    public static PlayerGhostRecorder Instance;

    [System.Serializable]
    public struct PlayerState
    {
        public Vector3 position;
        public bool isJumping;
        public bool isRunning;
    }

    public float recordInterval = 0.1f;
    private float timer;
    private List<PlayerState> recordedStates = new List<PlayerState>();

    private PlayerMovement playerMovement; // dein eigenes Movement-Skript

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= recordInterval)
        {
            timer = 0f;
            recordedStates.Add(new PlayerState
            {
                position = transform.position,
                isJumping = playerMovement.IsJumping(),   // musst du bereitstellen!
                isRunning = playerMovement.IsRunning()    // musst du bereitstellen!
            });
        }
    }

    public List<PlayerState> GetRecordedStates()
    {
        return recordedStates;
    }
}
