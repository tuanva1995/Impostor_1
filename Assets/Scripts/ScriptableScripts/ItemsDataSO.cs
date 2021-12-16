using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemData
{
    public ItemKind kindItem;
    public float itemTime, itemValue;
}
[System.Serializable]
public struct TrapData
{
    public TrapKind kindTrap;
    public int damageValue;
    public float cooldownTime;
}
[CreateAssetMenu(fileName = "ItemsData", menuName = "ScriptableObjects/ItemsDataSO", order = 2)]
public class ItemsDataSO : ScriptableObject
{
    public ItemData[] itemDataArray = new ItemData[4];
    public TrapData[] trapDataArray;
}
