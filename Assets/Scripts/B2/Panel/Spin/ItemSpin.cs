using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
public class ItemSpin : MonoBehaviour
{
    [SerializeField] GameObject boder, content;
    [SerializeField] TextMeshProUGUI textCoin;
    [SerializeField] Image imageItem;

    SpinModel data;
    
    public void SetContent(SpinModel _model)
    {
        this.data = _model;
        textCoin.text = data.coin.ToString();
    }
    public void ResetItem()
    {
        boder.SetActive(false);
        content.SetActive(false);
    }
    public void SelectItem()
    {
        boder.SetActive(true);
        content.SetActive(true);
    }
    
}
