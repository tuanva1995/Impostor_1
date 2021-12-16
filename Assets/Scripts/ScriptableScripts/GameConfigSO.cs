using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CharacterParameter
{
    public Kind_Character kindCharacter;
    public float oriHP, oriMaxHP;
    public float oriDamage;
    public float oriAttackSpeed, oriMoveSpeed;
    public float oriAttackDistance;
}

[CreateAssetMenu(fileName = "GameConfigSO", menuName = "ScriptableObjects/GameConfigSO", order = 1)]
public class GameConfigSO : ScriptableObject
{
    [Header("Character Parameters")]
    public CharacterParameter[] characterParamArray;
    [Header("Game Config")]
    public int victoryBonusExp;
    public int defeatBonusExp;
    public int defeatBonusGold;
    public int watchVideoBonusGold;
    public float[] timeRounds;
}
