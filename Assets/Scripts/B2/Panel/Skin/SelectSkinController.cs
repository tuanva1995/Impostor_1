using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkinController : MonoBehaviour
{
    //public SkinScriptable skinData;
    //public HatScriptable hatData;
    [SerializeField] GameObject scrollSkin, scrollOutfit;
    [SerializeField] List<ItemSkin> listItemSkin;
    [SerializeField] List<ItemHat> listItemHat;
    [SerializeField] Sprite[] spriteTitleSkin, spriteTitleOutfit;
    [SerializeField] Image imageTitleSkin, imageTitleOutfit;
    public PlayerSkin player;
    [SerializeField] Transform transPlayer;
    [SerializeField] Camera camera;

    public static SelectSkinController Instance;

    private void Start()
    {
        Instance = this;
    }
    void SetCameraCanvas(Camera _camera = null)
    {
        if(_camera == null)
        {
            transform.parent.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            camera.gameObject.SetActive(false);

        }
        else
        {
            camera.gameObject.SetActive(true);
            transform.parent.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            transform.parent.GetComponent<Canvas>().worldCamera = _camera;
        }
    }

    private void OnEnable()
    {
        btnScrollSelect_OnClick(0);
        SetPanel();
        SetCameraCanvas(camera);
    }
    private void OnDisable()
    {
        MainCanvas.Instance.SetSkinAndHatPlayer();
        SetCameraCanvas(null);
    }
    void SetPanel()
    {
        player.transform.position  = new Vector3(transPlayer.position.x, transPlayer.position.y, player.transform.position.z);
    }
    

    


    public void SetScrollSkin()
    {
        for(int i=0; i< listItemSkin.Count; i++)
        {
            listItemSkin[i].SetItem(DataManager.Instance.dataSkin.datas[i+1]); // bat dau tu skin data 1
        }
        player.SetUpSkin();
        Debug.Log("====SetScrollSkin");
        //MainCanvas.Instance.SetSkinAndHatPlayer();
    }
    public void SetScrollHat()
    {
        for (int i = 0; i < listItemHat.Count; i++)
        {
            listItemHat[i].SetItem(DataManager.Instance.dataHat.datas[i + 1]); // bat dau tu skin data 1
        }
        player.SetUpSkin();
        Debug.Log("====SetScrollSkin");
    }

    public void btnScrollSelect_OnClick(int indexScroll)
    {
        scrollSkin.SetActive(indexScroll == 0);
        scrollOutfit.SetActive(indexScroll == 1);
        imageTitleSkin.sprite = indexScroll == 0 ? spriteTitleSkin[0] : spriteTitleSkin[1];
        imageTitleOutfit.sprite = indexScroll == 1 ? spriteTitleOutfit[0] : spriteTitleOutfit[1];
        SoundController.Instance.PlaySfxButtonClick();

        if(indexScroll == 0)
        {
            SetScrollSkin();
        }else if(indexScroll == 1)
        {
            SetScrollHat();
        }

    }

    public void btnClose_Onclick()
    {
        gameObject.SetActive(false);
        SoundController.Instance.PlaySfxButtonClick();

    }



}
