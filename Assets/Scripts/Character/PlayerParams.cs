using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerParams : CharacterParams
{
    public override Kind_Character kindCharacter { get { return Kind_Character.Player; } }

    protected override void CustomSetParams()
    {
        if (DataController.Instance == null) return;
        hp = maxHP = DataController.Instance.playerData.playerHPCurrentIndex;
        damage = DataController.Instance.playerData.playerDameCurrentIndex;
        if (SceneManager.GetActiveScene().name != "TestSkinScene")
            avatar = GameController.Instance.PlayerAvatar;
    }
}
