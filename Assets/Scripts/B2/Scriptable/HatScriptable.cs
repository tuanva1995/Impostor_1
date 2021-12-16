using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HatData", menuName = "ScriptableObjects/HatData")]
public class HatScriptable : ScriptableObject
{
    public List<DataHatModel> datas;
    public DataHatModel GetDataHat(int id)
    {
        DataHatModel data = datas.Find(x => x.id == id);
        return data;
    }

    public int idCurrentHat
    {
        set
        {
            PlayerPrefs.SetInt("IdCurrentHat", value);
        }
        get
        {
            return PlayerPrefs.GetInt("IdCurrentHat", 0);
        }
    }
}

public enum StateHatItem
{
    LOCK,
    UNLOCKED
}


[System.Serializable]
public class DataHatModel
{
    public int id;
    public string name;
    public string description;
    public int amountAds;
    public Sprite sprite;
    public GameObject prefabPlayer;
    public float dame;
    public float hp;
    public int price;
    public bool getSpin;
    public bool getShop;

    public DataHatModel(DataHatModel objectP)
    {
        this.id = objectP.id;
        this.name = objectP.name;
        this.description = objectP.description;
        this.amountAds = objectP.amountAds;
        this.sprite = objectP.sprite;
        this.dame = objectP.dame;
        this.hp = objectP.hp;
        this.prefabPlayer = objectP.prefabPlayer;
    }

    public StateHatItem State
    {
        get
        {
            //if(CountViewAds >= amountAds && PlayerPrefs.GetInt("SkinUnLock" + id) != 2)
            //{
            //    return StateSkinItem.UNLOCKED;
            //}
            return (StateHatItem)PlayerPrefs.GetInt("HatUnLock" + id, 0);
        }
        set
        {
            PlayerPrefs.SetInt("HatUnLock" + id, (int)value);
            PlayerPrefs.Save();
        }
    }


    public int CountViewAds
    {
        set
        {
            PlayerPrefs.SetInt("CountViewAdsHat" + id, value);
        }
        get
        {
            return PlayerPrefs.GetInt("CountViewAdsHat" + id, 0);
        }
    }

    //public void Select()
    //{
    //    var oldPlayer = GameController.Instance.dataContains.dataPlayer.GetDataPlayer(DataManager.CurrentIDPlayer);
    //    if (oldPlayer.State != StatePlayerItem.LOCK)
    //        oldPlayer.UnSelect();

    //    DataManager.CurrentIDPlayer = ID;
    //    State = StatePlayerItem.SELECTED;

    //}

    //public void UnSelect()
    //{
    //    State = StatePlayerItem.UNLOCKED;
    //}
}
