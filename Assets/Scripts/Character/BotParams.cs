using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotParams : CharacterParams
{
    public Kind_Character charKind;
    public override Kind_Character kindCharacter { get { return charKind; } }

    protected override void CustomSetParams()
    {
        if (SceneManager.GetActiveScene().name != "TestSkinScene")
        {
            maxHP = hp += UserData.NextLevel * 0.1f * hp;
            damage += UserData.NextLevel * 0.1f * damage;
        }
    }
}
