
using System.Collections;
using UnityEngine;

public class SpikeGenerator : MonoBehaviour
{
    public GameObject spike;
    public float SpeedMultiplier;
    public float MaxSpeed;
    public float MinSpeed;
    public float currentSpeed;

// function steine und nicht collideren 

    void Awake()
    {
        currentSpeed = MinSpeed;
        StartCoroutine(Spawner());
    }

    public void generateSpike()
    {
        GameObject SpikeIns = Instantiate(spike, transform.position, transform.rotation);
        SpikeIns.GetComponent<SpikeScript>().spikeGenerator = this;
    }
    // Update is called once per frame

    IEnumerator Spawner()
    {
        yield return new WaitForSecondsRealtime(Random.RandomRange(3, 5));
        generateSpike();
        StartCoroutine(Spawner());
    }

    void Update(){
        if (currentSpeed<MaxSpeed)
        {
            currentSpeed += SpeedMultiplier;
        }
    }

}
