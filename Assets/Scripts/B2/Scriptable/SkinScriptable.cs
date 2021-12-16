using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SkinData", menuName = "ScriptableObjects/SkinData")]
public class SkinScriptable : ScriptableObject
{
    public List<DataSkinModel> datas;
    public DataSkinModel GetDataSkin(int id)
    {
        DataSkinModel data = datas.Find(x => x.id == id);
        return data;
    }
    public int idCurrentSkin
    {
        set
        {
            PlayerPrefs.SetInt("IdCurrentSkin", value);
        }
        get
        {
            return PlayerPrefs.GetInt("IdCurrentSkin", 0);
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

public enum StateSkinItem
{
    LOCK,
    UNLOCKED
}


[System.Serializable]
public class DataSkinModel
{
    public int id;
    public string name;
    public string description;
    public int amountAds;
    public Sprite spriteSkin;
    public GameObject prefabPlayer;
    public float dame;
    public float hp;
    public int price;
    public bool getSpin;
    public bool getShop;

    public DataSkinModel(DataSkinModel objectP)
    {
        this.id = objectP.id;
        this.name = objectP.name;
        this.description = objectP.description;
        this.amountAds = objectP.amountAds;
        this.spriteSkin = objectP.spriteSkin;
        this.dame = objectP.dame;
        this.hp = objectP.hp;
        this.prefabPlayer = objectP.prefabPlayer;
    }

    public StateSkinItem State
    {
        get
        {
            //if(CountViewAds >= amountAds && PlayerPrefs.GetInt("SkinUnLock" + id) != 2)
            //{
            //    return StateSkinItem.UNLOCKED;
            //}
            return (StateSkinItem)PlayerPrefs.GetInt("SkinUnLock" + id, 0);
        }
        set
        {
            PlayerPrefs.SetInt("SkinUnLock" + id, (int)value);
            PlayerPrefs.Save();
        }
    }


    public int CountViewAds
    {
        set
        {
            PlayerPrefs.SetInt("CountViewAdsSkin" + id, value);
        }
        get
        {
            return PlayerPrefs.GetInt("CountViewAdsSkin" + id, 0);
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
