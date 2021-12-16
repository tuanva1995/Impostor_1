using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerDataSO", order = 1)]
public class PlayerDataSO : ScriptableObject
{
    public Texture[] PlayerSkins;
    public Sprite[] PlayerAvatars;
    public Sprite[] PlayerFlags;
    public Sprite[] PlayerFlagsRectangle;
}
