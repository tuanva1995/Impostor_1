using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BotData", menuName = "ScriptableObjects/BotDataSO", order = 1)]
public class BotDataSO : ScriptableObject
{
    public Material[] botMaterials;
    public Sprite[] botAvatars;
}
