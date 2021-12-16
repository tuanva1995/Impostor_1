using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Combo : MonoBehaviour
{
    public Image icon;
    public Sprite[] lsSprs;
    // Start is called before the first frame update
    void OnEnable()
    {
        //icon.gameObject.SetActive(false);
        //icon.sprite = lsSprs[Random.Range(0, lsSprs.Length)];
    }
    public void SetCombo()
    {
        GetComponent<Animator>().Play("Combo");
        //gameObject.SetActive(false);
        icon.sprite = lsSprs[Random.Range(0, lsSprs.Length)];
        icon.SetNativeSize();
        //icon.transform.DOScale(1,0.5f).Complete
    }
    
}
