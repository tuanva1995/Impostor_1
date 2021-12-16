using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;

public class Confirm : MonoBehaviour
{
    [SerializeField] GameObject popup;
    [SerializeField] Text txtYes;
    [SerializeField] Text txtNo;
    [SerializeField] Button btnYes;
    [SerializeField] Button btnNo;

    [SerializeField] GameObject title;
    [SerializeField] Text txtTitle;
    [SerializeField] Text txtContent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetMessenger(string content, bool showBtnYes = true, bool showBtnNo = false, string txtBtnYes = "No", string txtBtnNo = "Oke", UnityEngine.Events.UnityAction eventYes = null, UnityEngine.Events.UnityAction eventNo = null)
    {
        btnYes.onClick.RemoveAllListeners();
        btnNo.onClick.RemoveAllListeners();
        txtContent.text = content;
        btnYes.gameObject.SetActive(showBtnYes);
        btnNo.gameObject.SetActive(showBtnNo);
        txtYes.text = txtBtnYes;
        txtNo.text = txtBtnNo;
        btnYes.onClick.AddListener(eventYes);
        btnNo.onClick.AddListener(eventNo);
        btnYes.onClick.AddListener(HidePopup);
        btnNo.onClick.AddListener(HidePopup);
        if (eventYes == null)
        {
            btnYes.onClick.RemoveAllListeners();
            btnYes.onClick.AddListener(HidePopup);
        }
        if (eventNo == null)
        {
            btnNo.onClick.RemoveAllListeners();
            btnNo.onClick.AddListener(HidePopup);
        }
    }

    private void OnEnable()
    {
        ShowPopup();
    }

    void ShowPopup()
    {
        btnYes.interactable = true;
        btnNo.interactable = true;
        Commons.ShowPopup(popup.transform);
        //popup.transform.localPosition = new Vector3(0, 500, 0);
        //popup.transform.DOLocalMoveY(0, 0.3f);
    }
    public void HidePopup()
    {
        btnYes.interactable = false;
        btnNo.interactable = false;
        Commons.HidePopup(popup.transform, this.gameObject);
    }
}
