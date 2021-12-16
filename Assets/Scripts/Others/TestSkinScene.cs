using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkinScene : InitScenePlay
{
    // Start is called before the first frame update
    void Awake()
    {
        FindObjectOfType<ShowGamePlayData>().levelTime = 120;
        var playerControl = FindObjectOfType<PlayerControl>();
        playerControl.SetSkinPlayer(DataController.Instance.testSkinID);
        playerControl.GetComponent<PlayerMovement>().enabled = true;
        SetStartScene();
    }

}
