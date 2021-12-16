using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Gift : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textTimeCoundown;
    int coinGift = 20;
    // Start is called before the first frame update
    bool HaveGetGift
    {
        set
        {
            PlayerPrefs.SetInt("HaveGetGift", value == true ? 1 : 0);
        }
        get
        {
            return PlayerPrefs.GetInt("HaveGetGift", 0) == 1;
        }
    }
    void Start()
    {
        SetItem();
    }
    void SetItem()
    {
        textTimeCoundown.gameObject.SetActive(HaveGetGift);
    }
    public void btnGetGift_Onclick()
    {
        if (HaveGetGift)
        {
            Messenger.Instance.SetMess("You received your gift today!");
            return;
        }
        else
        {
            UserData.Coin += coinGift;
            EffectItemFly.Instance.StartEffectItemFly(transform.position, MainCanvas.Instance.transCoin.position, TypeItemEffectFly.coin, coinGift);
        }
        HaveGetGift = true;
        textTimeCoundown.gameObject.SetActive(true);
    }
    void SetTime()
    {
        textTimeCoundown.text = Commons.FormatTimeSecond((int)Commons.timeCountDownNextDay());
    }

    // Update is called once per frame
    void Update()
    {
        if (textTimeCoundown.gameObject.activeInHierarchy)
        {
            SetTime();
        }
        
    }
}
