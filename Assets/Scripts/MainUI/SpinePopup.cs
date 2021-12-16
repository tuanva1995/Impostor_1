using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpinePopup : SimplePopup
{
    public Transform spineTrans;
    protected override void Start()
    {
        base.Start();
        Spin();
    }
    public void Spin()
    {
       
    }
    //IEnumerator CoSpine()
    //{

    //}
}
