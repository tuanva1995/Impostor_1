using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkin : MonoBehaviour
{
    public SkinnedMeshRenderer[] lsMeshs;
    public MeshRenderer[] lsMeshsRen;
    public Material[] lsMat;
    public GameObject[] lsHats;
    void Start()
    {
        int rdMat = Random.Range(0, lsMat.Length);
        foreach (SkinnedMeshRenderer skin in lsMeshs)
        {
            skin.sharedMaterial = lsMat[rdMat];
        }
        foreach (MeshRenderer skin in lsMeshsRen)
        {
            skin.sharedMaterial = lsMat[rdMat];
        }
        int rd = Random.Range(0, lsHats.Length);
        for (int i = 0; i < lsHats.Length; i++)
        {
            if (i == rd)
                lsHats[i].SetActive(true);
            else
                lsHats[i].SetActive(false);
        }
    }
}
