using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlagPopup : SimplePopup
{
    public GameObject itemFLag;
    public Transform contentFlag;
    public Image flagButtonImg;
    public PlayerDataSO data;
    Sprite[] lsFlag;
    List<FlagItem> lsFlagItem;
    protected override void Start()
    {
        base.Start();
        lsFlag = data.PlayerFlagsRectangle;
        lsFlagItem = new List<FlagItem>();
        SetUpItem();
    }
    void SetUpItem()
    {
        flagButtonImg.sprite = lsFlag[DataController.Instance.playerData.FlagIndex];
        for (int i = 0; i < lsFlag.Length; i++)
        {
            GameObject item =  Instantiate(itemFLag) as GameObject;
            item.transform.SetParent(contentFlag);
            FlagItem flagItem = item.GetComponent<FlagItem>();
            lsFlagItem.Add(flagItem);
            flagItem.SetUp(i,lsFlag[i]);
            int temp = i;
            item.GetComponent<Button>().onClick.AddListener(() => {
                lsFlagItem[DataController.Instance.playerData.FlagIndex].stick.SetActive(false);
                lsFlagItem[DataController.Instance.playerData.FlagIndex].cover.SetActive(false);
                DataController.Instance.playerData.FlagIndex = temp;
                flagItem.stick.SetActive(true);
                flagItem.cover.SetActive(true);
                flagButtonImg.sprite = lsFlag[temp];
                //DataController.Instance.playerData.FlagIndex = temp;
                DataController.Instance.SaveData();
            });
        }
    }
}
