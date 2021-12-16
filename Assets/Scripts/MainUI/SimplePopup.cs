using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public enum AnimationPopupType
{
    OnTopDown,
    OnFade
}
public class SimplePopup : MonoBehaviour
{
	
    [SerializeField]
    protected CanvasGroup content;
    [SerializeField]
    private Image cover;
    [SerializeField]
    private Text txtContent;
	
    [SerializeField]
    private GameObject gridBtn;
    [SerializeField]
    protected UIButtonSimple btnYes;
    [SerializeField]
    protected UIButtonSimple btnNo;
    [SerializeField]
    public UIButtonSimple btnOK;
    /// Is this pop-up closed when player hits Back button on device?
    public bool IsClosedOnEsc;
    public SimplePopup ParentPopup;
    private bool parentCloseOnEsc { get; set; }
    private Action onShown { get; set; }
    private Action onHidden { get; set; }
    private float contentStartY { get; set; }
    private float yPos { get; set; }
    public bool IsActive { get; set; }
    public AnimationPopupType animType = AnimationPopupType.OnTopDown;
    protected virtual void Start()
    {
        contentStartY = content.transform.localPosition.y;
        yPos = 400f + contentStartY;
    }

    /// <summary>
    /// Show the pop-up. This method should be called at the end, AFTER all assignments to this pop-up (especially adding button callbacks) have been made.
    /// </summary>
    /// <param name="content">String of content</param>
    /// <param name="isYesNo">Is this consisting of 2 buttons?</param>
    /// <param name="isStatus">Is this pop-up a status display for confirmation only?</param>
    /// <param name="isError">Is this a warning/error popping?</param>
    public virtual void ShowUp(string content = "", bool isYesNo = true, bool isStatus = false, AnimationPopupType type = AnimationPopupType.OnTopDown)
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxPopup);
        this.content.blocksRaycasts = true;
        animType = type;
        if (!string.IsNullOrEmpty(content))
        {
            txtContent.text = content;
        }
        if (btnOK != null)
        {
            btnOK.gameObject.SetActive(isStatus);
            AddOnButtonOK(Hide);
        }
        if (btnYes != null)
        {
            btnYes.gameObject.SetActive(isYesNo);
            AddOnButtonYes(Hide);
        }
        if (btnNo != null)
        {
            btnNo.gameObject.SetActive(isYesNo);
            AddOnButtonNo(Hide);
        }
       
        ShowUpAnimations(type);
      
    }
    public virtual void ShowPopup(AnimationPopupType type = AnimationPopupType.OnTopDown)
    {
        ShowUp(null, false, false, type);
    }
    private void ShowUpAnimations(AnimationPopupType type)
    {
        switch (type)
        {
            case AnimationPopupType.OnTopDown:
                cover.DOKill(false);
                content.DOKill(false);
                content.transform.DOKill(false);
                cover.gameObject.SetActive(true);
                cover.DOFade(0.95f, 0.2f).SetUpdate(true);
                content.transform.localPosition = new Vector3(0, yPos);

                content.gameObject.SetActive(true);
                content.DOFade(1, 0.15f).SetUpdate(true);
                content.transform.DOLocalMoveY(contentStartY, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    IsActive = true;
                    //Global.IsClosedOnEsc = IsClosedOnEsc = true;
                    OnCompletedButton();
                    if (onShown != null)
                    {
                        onShown();

                    }
                }).SetUpdate(true);
                break;
            case AnimationPopupType.OnFade:
                cover.DOKill(false);
                content.DOKill(false);
                content.transform.DOKill(false);
                cover.gameObject.SetActive(true);
                cover.DOFade(0.95f, 0.2f).SetUpdate(true);
                content.transform.localScale = new Vector3(0, 0);
                content.gameObject.SetActive(true);
                content.DOFade(1, 0.15f).SetUpdate(true);
                content.transform.DOScale(new Vector3(1,1), 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    IsActive = true;
                   // Global.IsClosedOnEsc = IsClosedOnEsc = true;
                    OnCompletedButton();
                    if (onShown != null)
                    {
                        onShown();

                    }
                }).SetUpdate(true);
                break;
        }
    }
        
    public virtual void OnCompletedButton()
    {

    }
    public virtual void OnCloseButton()
    {
        Hide();
    }

    public virtual void OnNoButton()
    {
        Hide();
    }

    public virtual void OnYesButton()
    {
        Hide();
    }
    public virtual void Hide()
    {
        MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxClickButton);
        HideAnimations(animType);
      
    }

    private void HideAnimations(AnimationPopupType type )
    {
        switch (type)
        {
            case AnimationPopupType.OnTopDown:
                //Global.IsClosedOnEsc = IsClosedOnEsc = false;
                cover.DOKill(false);
                content.DOKill(false);
                content.transform.DOKill(false);
                cover.DOFade(0, 0.1f).SetUpdate(true);
                content.DOFade(0, 0.1f).SetDelay(0.2f).SetUpdate(true);

                if (onHidden != null)
                {
                    onHidden();
                    onHidden = null;
                }
                content.transform.DOLocalMoveY(yPos, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    IsActive = false;
                    cover.gameObject.SetActive(false);
                    content.blocksRaycasts = false;
                    content.gameObject.SetActive(false);
                    txtContent.text = "";
                    if (onHidden != null)
                    {
                        onHidden();
                        onHidden = null;
                    }

                }).SetUpdate(true);
                break;
            case AnimationPopupType.OnFade:
                //Global.IsClosedOnEsc = IsClosedOnEsc = false;
                cover.DOKill(false);
                content.DOKill(false);
                content.transform.DOKill(false);
                cover.DOFade(0, 0.1f).SetUpdate(true);
                content.DOFade(0, 0.1f).SetDelay(0.2f).SetUpdate(true);

                if (onHidden != null)
                {
                    onHidden();
                    onHidden = null;
                }
                content.transform.DOScale(new Vector3(0, 0), 0.2f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    IsActive = false;
                    cover.gameObject.SetActive(false);
                    content.blocksRaycasts = false;
                    content.gameObject.SetActive(false);
                    txtContent.text = "";
                    if (onHidden != null)
                    {
                        onHidden();
                        onHidden = null;
                    }

                }).SetUpdate(true);
                break;
        }
       
    }

    public void SetOnShown(Action onShown)
    {
        this.onShown = null;
        this.onShown += onShown;
    }

    public void SetOnHidden(Action onHidden)
    {
        this.onHidden = null;
        this.onHidden += onHidden;
    }

    public void SetOnButtonYes(UnityAction callback)
    {
        btnYes.onPress.RemoveAllListeners();
        btnYes.onPress.AddListener(callback);
        btnYes.onPress.AddListener(()=> { Hide(); });
    }

    public void SetOnButtonNo(UnityAction callback)
    {
        //SoundController.Instance.PlaySingle(UISfxController.Instance.SfxButtonSound);
        btnNo.onPress.RemoveAllListeners();
        btnNo.onPress.AddListener(callback);
        btnNo.onPress.AddListener(() => { Hide(); });
    }

    public void SetOnButtonOK(UnityAction callback)
    {
        btnOK.onPress.RemoveAllListeners();
        btnOK.onPress.AddListener(callback);
        btnOK.onPress.AddListener(() => { Hide(); });
    }

    public void AddOnButtonYes(UnityAction callback)
    {
       // Debug.Log("Set btn ok");
        btnYes.onPress.AddListener(callback);
    }

    public void AddOnButtonNo(UnityAction callback)
    {
        //SoundController.Instance.PlaySingle(UISfxController.Instance.SfxClickSound);
        btnNo.onPress.AddListener(callback);
    }

    public void AddOnButtonOK(UnityAction callback)
    {
        btnOK.onPress.AddListener(callback);
    }

    private void Update()
    {
        if (IsActive
            && IsClosedOnEsc
            && Input.GetKeyDown(KeyCode.Escape))
        {
			
            OnCloseButton();
        }
    }
}
