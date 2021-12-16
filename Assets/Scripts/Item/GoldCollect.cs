using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollect : MonoBehaviour
{
    private SpawnGold goldSpawnScript;

    private void Awake()
    {
        goldSpawnScript = GetComponentInParent<SpawnGold>();
    }
    private void OnTriggerEnter(Collider other)
    {
        var goldCollector = other.GetComponent<ICollectGold>();
        if (goldCollector != null)
        {
            goldCollector.CollectGold();
            if (goldSpawnScript.currentGoldAmount > 0)
                goldSpawnScript.currentGoldAmount--;
            Destroy(gameObject);
        }
    }
}
