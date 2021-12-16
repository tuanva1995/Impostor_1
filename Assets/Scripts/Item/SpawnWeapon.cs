using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnWeapon : MonoBehaviour
{
    [SerializeField] private GameObject[] ListWeapons;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        if (LevelManager.GetCurrentLevelData().levelID <= 5)
        {
            UseWeaponItem();
        }
        EventDisPatcher.Instance.RegisterListener(EventID.OnUseWeapon, (obj) => UseWeaponItem());
    }

    // Update is called once per frame
    void Update()
    {
        //if(LevelManager.GetCurrentLevelData().levelID <= 5)
        //{
        //    if (!FindObjectOfType<WeaponCollect>())
        //    {
        //        currentTime += Time.deltaTime;
        //    }
        //    else
        //    {
        //        currentTime = 0;
        //    }
        //    if (currentTime > 10)
        //    {
        //        UseWeaponItem();
        //        currentTime = 0;
        //    }
        //}
    }

    void UseWeaponItem()
    {
        //GameObject wp = Instantiate(ListWeapons[Random.Range(0, ListWeapons.Length)]);
        ////GameObject wp = Instantiate(ListWeapons[1]);
        //Vector3 randomPoint = GetRandomLocation();
        //wp.transform.position = new Vector3(randomPoint.x, wp.transform.position.y, randomPoint.z);
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

}
