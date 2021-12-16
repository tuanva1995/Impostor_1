using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnGold : MonoBehaviour
{
    [SerializeField] private GameObject GoldObj;
    [SerializeField] private float timeSpawnDelay = 3f;

    private int maxGoldAmount;
    public int currentGoldAmount;
    private bool isSpawning;

    // Start is called before the first frame update
    void Start()
    {
        var levelData = LevelManager.GetCurrentLevelData();
        maxGoldAmount = levelData.maxDiamond;
        for(int i = 0; i< levelData.beginDiamond; i++)
        {
            InstantiateGold();
        }
    }

    private void Update()
    {
        if (currentGoldAmount < maxGoldAmount && !isSpawning)
        {
            StartCoroutine(SpawnNewGold());
        }
    }
    void InstantiateGold()
    {
        GameObject item = Instantiate(GoldObj, transform);
        Vector3 randomPoint = GetRandomLocation();
        item.transform.position = new Vector3(randomPoint.x, item.transform.position.y, randomPoint.z);
        currentGoldAmount++;
    }
    Vector3 GetRandomLocation()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
        // Pick the first indice of a random triangle in the nav mesh
        int t = Random.Range(0, navMeshData.areas.Length);
        // Select a random point on it
        Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t * 3]], navMeshData.vertices[navMeshData.indices[t * 3 + 1]], 0.5f);
        point = Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t * 3 + 2]], 0.5f);

        return point;
    }

    IEnumerator SpawnNewGold()
    {
        isSpawning = true;
        yield return new WaitForSeconds(timeSpawnDelay);
        InstantiateGold();
        isSpawning = false;
    }

}
