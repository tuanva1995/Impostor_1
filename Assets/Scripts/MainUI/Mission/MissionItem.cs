using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionItem : MonoBehaviour
{
    //public PayOuts type;
    public Image icon;
    public Text info;
    public Text rewardValue;
    public Text btnTxt;
    public UIButtonSimple button;
    public bool isReward;
    
    public void SetUp(string info, double process, int target,int reward,Sprite spr )
    {
        icon.sprite = spr;
        this.info.text = string.Format(info,target);
        rewardValue.text = reward.ToString();
        btnTxt.text = process + "/" +target;
        gameObject.transform.localScale = new Vector3(1, 1);
    }
}
