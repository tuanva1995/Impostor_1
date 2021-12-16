using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlagItem : MonoBehaviour
{
    int id;
    public Image icon;
    public GameObject stick, cover;
    public void SetUp(int id,Sprite flg)
    {
        this.id = id;
        icon.sprite = flg;
        transform.localScale = new Vector3(1, 1);
        if (id == DataController.Instance.playerData.FlagIndex)
        {
            stick.SetActive(true);
            cover.SetActive(true);
        }
    }

}
