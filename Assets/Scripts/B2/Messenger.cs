using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Messenger : MonoBehaviour
{
    public static Messenger Instance;
    [SerializeField] TextMeshProUGUI txtContent;
    [SerializeField] Transform popup;
    bool isStart;

    void Start()
    {
        Instance = this;
        isStart = false;
        c = ResetPopup();
        
    }
    public void SetMess(string _txtContent)
    {
        this.StopAllCoroutines();
        popup.DOKill();
        //StopCoroutine(c);
        popup.gameObject.SetActive(true);
        isStart = false;
        popup.transform.localPosition = new Vector3(0, 0,0);
        this.txtContent.text = _txtContent;
        popup.DOLocalMoveY(200, 0.3f).OnComplete(() =>
        {
            StartCoroutine(ResetPopup());
           // popup.gameObject.SetActive(false);
        }).OnKill(() =>
        {
            //StartCoroutine(ResetPopup());
            //popup.gameObject.SetActive(false);
        });
    }
    IEnumerator c;
    IEnumerator ResetPopup()
    {
        yield return new WaitForSeconds(0.7f);
        popup.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
