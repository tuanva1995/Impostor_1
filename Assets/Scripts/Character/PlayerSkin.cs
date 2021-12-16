using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    public SkinnedMeshRenderer[] lsMeshs;
    public Material[] lsMat;
    public GameObject[] lsHats;
    public MeshRenderer[] lsMeshsRen;
    void Start()
    {
        SetUpSkin();
    }
    public void SetUpSkin()
    {
        if (DataManager.Instance == null) return;
        int rdMat = DataManager.Instance.dataSkin.idCurrentSkin;
        foreach (SkinnedMeshRenderer skin in lsMeshs)
        {
            skin.sharedMaterial = lsMat[rdMat];
        }
        foreach (MeshRenderer skin in lsMeshsRen)
        {
            skin.sharedMaterial = GameController.Instance.matOndie[rdMat];
        }
        for (int i = 0; i < lsHats.Length; i++)
        {
            if (i == DataManager.Instance.dataHat.idCurrentHat-1)
                lsHats[i].SetActive(true);
            else
                lsHats[i].SetActive(false);
        }
    }
}
