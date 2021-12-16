using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "SpinData", menuName = "ScriptableObjects/SpinData")]

public class SpinScriptable : ScriptableObject
{
    public int maxReward;
    public int getRewardSkin;
    public int getRewardHat;
    public List<SpinModel> datas;


    public int countSpinAds
    {
        set
        {
            PlayerPrefs.SetInt("countSpinAds", value);
        }
        get
        {
            return PlayerPrefs.GetInt("countSpinAds", 5);
        }
    }
    public double timeCountDownFree{
        set{
            PlayerPrefs.SetString("timeCountDownFreeSpin", value.ToString());
        }
        get{
           return double.Parse(PlayerPrefs.GetString("timeCountDownFreeSpin", "0"));
        }
    }
    public int countSpinFree
    {
        set
        {
            PlayerPrefs.SetInt("countSpinFree", value);
        }
        get
        {
            return PlayerPrefs.GetInt("countSpinFree", 1);
        }
    }
    public int countSpin
    {
        set
        {
            PlayerPrefs.SetInt("countSpin", value);
        }
        get
        {
            return PlayerPrefs.GetInt("countSpin", 0);
        }
    }
}

[Serializable]
public class SpinModel{
    public int id;
    public int coin;
}
