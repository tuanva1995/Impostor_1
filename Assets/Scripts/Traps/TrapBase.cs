using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBase : MonoBehaviour
{
    [SerializeField] private ItemsDataSO trapDatas;
    protected TrapData trapData;

    protected InitScenePlay InitPlay;

    public virtual TrapKind trapKind { get; }

    protected void Awake()
    {
        InitPlay = FindObjectOfType<InitScenePlay>();
        foreach (var data in trapDatas.trapDataArray) if (data.kindTrap == trapKind) trapData = data;
    }
    protected void Update()
    {
        if (InitPlay) if (!InitPlay.isStart) return;
    }
}
