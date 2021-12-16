using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public static int Coin
    {
        set
        {
            int _coin = value;
            if (_coin < 0) _coin = 0;
            PlayerPrefs.SetInt("COIN", _coin);
        }
        get
        {
            return PlayerPrefs.GetInt("COIN", 0);
        }
    }
    public static int NextLevel
    {
        set
        {
          
            PlayerPrefs.SetInt("NextLevel", value);
        }
        get
        {
            return PlayerPrefs.GetInt("NextLevel", 1);
        }
    }
    public static bool RemoveAdsState
    {
        set
        {
            PlayerPrefs.SetInt("RemoveAdsState", value == true ? 1 : 0);
        }
        get
        {
            return PlayerPrefs.GetInt("RemoveAdsState", 0) == 1;
        }
    }
   


}
