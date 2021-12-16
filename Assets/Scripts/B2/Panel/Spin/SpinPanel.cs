using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class SpinPanel : MonoBehaviour
{
    [SerializeField] SpinScriptable dataSpin;
    [SerializeField] ItemSpin[] arrayItemSpin;
    [SerializeField] GameObject[] boders;
    [SerializeField] Button buttonFreeSpin, buttonSpinAds;
    [SerializeField] TextMeshProUGUI textAmountSpinAds;
    [SerializeField] GameObject notiAds;
    [SerializeField] TextMeshProUGUI textCountDownFreeSpin;
    [SerializeField] Image valueBar;
    private void Update()
    {
        if (textCountDownFreeSpin.gameObject.activeInHierarchy)
        {
            SetTimeCounDownFree();
        }
    }
    private void OnEnable()
    {
        SetPanel();
        for (int i = 0; i < arrayItemSpin.Length; i++)
        {
            arrayItemSpin[i].ResetItem();
        }
        UpdateBarRewead();
    }
    private void OnDisable()
    {
        this.StopAllCoroutines();
    }
    void ResetContenItems()
    {
        
    }
    void SetPanel()
    {
        buttonFreeSpin.interactable = buttonSpinAds.interactable = true;
        buttonSpinAds.interactable = (dataSpin.countSpinAds > 0);

        buttonFreeSpin.interactable = (dataSpin.countSpinFree > 0);
        textCountDownFreeSpin.gameObject.SetActive(dataSpin.countSpinFree <= 0);
        textAmountSpinAds.text = dataSpin.countSpinAds.ToString();
        textAmountSpinAds.transform.parent.gameObject.SetActive(dataSpin.countSpinAds > 0);

    }
    void StartSpin(){
        int _indexItemSelect = UnityEngine.Random.Range(0, arrayItemSpin.Length);
        Debug.Log("item select: " + _indexItemSelect);
        StartCoroutine(Spin(_indexItemSelect));
    }
    public void btnSpinFree_Onclick()
    {
        if (dataSpin.countSpinFree <= 0) return;
        StartSpin();
        dataSpin.countSpinFree -= 1;
    }
    public void btnSpinAds()
    {
        if (dataSpin.countSpinAds <= 0) return;
        IronSourceAdsController.Instance.ShowVideoAds(AdsComplete, SetPanel);

    }
    void AdsComplete()
    {
        StartSpin();
        dataSpin.countSpinAds -= 1;
    }
    IEnumerator Spin(int _indexItem)
    {
        dataSpin.countSpin += 1;
        int coinAmount = 0;
        buttonFreeSpin.interactable= buttonSpinAds.interactable = false;
        for (int i = 0; i < arrayItemSpin.Length; i++)
        {
            int _indexDataRandom = UnityEngine.Random.Range(0, dataSpin.datas.Count);
            if (i == _indexItem)
            {
                coinAmount = dataSpin.datas[_indexDataRandom].coin;
                UserData.Coin += coinAmount;

                Debug.Log("amount coin:" + dataSpin.datas[_indexDataRandom].coin);
            }
            arrayItemSpin[i].SetContent(dataSpin.datas[_indexDataRandom]);
            arrayItemSpin[i].ResetItem();
        }
        foreach (ItemSpin item in arrayItemSpin)
        {
            item.ResetItem();
        }
        yield return new WaitForSeconds(0.05f);
        for(int i= 0; i<3; i++)
        {
            for(int j=0; j< boders.Length; j++)
            {
                boders[j].SetActive(true);
                yield return new WaitForSeconds(0.15f);
                boders[j].SetActive(false);
            }
            if(i == 2)
            {
                for (int j = 0; j < boders.Length; j++)
                {
                    if(j == _indexItem)
                    {
                        boders[j].SetActive(true);
                        yield return new WaitForSeconds(0.2f);
                        arrayItemSpin[_indexItem].SelectItem();
                        break;
                    }
                    else
                    {
                        boders[j].SetActive(true);
                        yield return new WaitForSeconds(0.2f);
                        boders[j].SetActive(false);
                    }
                }
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        EffectItemFly.Instance.StartEffectItemFly(arrayItemSpin[_indexItem].transform.position, MainCanvas.Instance.transCoin.position,TypeItemEffectFly.coin, coinAmount);
        SetPanel();
        UpdateBarRewead();
    }
    void UpdateBarRewead()
    {
        float _value = (float)dataSpin.countSpin / (float)dataSpin.maxReward;

        if (_value > 1) _value = 1;
        Debug.Log("valueBar.fillAmount : " + _value);

        valueBar.fillAmount = _value;
    }
    void SetTimeCounDownFree()
    {
        textCountDownFreeSpin.text = Commons.FormatTimeSecond((int)Commons.timeCountDownNextDay());
    }


    public void btnClose_Onclick()
    {
        gameObject.SetActive(false);
    }
}
