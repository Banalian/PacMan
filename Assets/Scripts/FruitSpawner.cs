using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{

    public GameObject[] availableFruits;
    public float respawnDelay = 10;
    
    private GameObject currentFruit = null;
    
    // Start is called before the first frame update
    void Awake()
    {
        CancelInvoke();
        Invoke(nameof(SpawnFruit), respawnDelay);
    }
    
    private void SpawnFruit()
    {
        int index = Random.Range(0, availableFruits.Length);

        currentFruit = Instantiate(availableFruits[index], this.transform);
    }

    public void NewFruit()
    {
        Destroy(currentFruit);
        CancelInvoke();
        Invoke(nameof(SpawnFruit), respawnDelay);
    }

    public void ResetState()
    {
        if (currentFruit != null)
        {
            Destroy(currentFruit);
        }
        CancelInvoke();
        Invoke(nameof(SpawnFruit), respawnDelay);
    }

}
