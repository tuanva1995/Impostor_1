using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FreeExChangePopup : SimplePopup
{
    public static FreeExChangePopup Instance;
    public Image img1, img2;
    public Sprite[] lsItem;
    protected override void Start()
    {
        Instance = this;
        base.Start();
    }
    public void SetUp(int index)
    {
        img1.sprite = img2.sprite = lsItem[index];
    }
    public override void ShowPopup(AnimationPopupType type = AnimationPopupType.OnTopDown)
    {
        base.ShowPopup(type);
    }
    
}
