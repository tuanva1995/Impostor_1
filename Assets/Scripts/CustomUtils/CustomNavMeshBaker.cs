using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// bake navmesh at runtime
/// </summary>
public class CustomNavMeshBaker : MonoBehaviour
{
    private NavMeshSurface[] surfaces;
    void Awake()
    {
        surfaces = FindObjectsOfType<NavMeshSurface>();
        foreach (var surface in surfaces) surface.BuildNavMesh();
    }
}
