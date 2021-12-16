using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeaponButton : ActiveItemButton
{
    public bool isPlayerCollect;
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private new void LateUpdate()
    {
        if (!isPlayerCollect) return;
        if (isActivating && item.itemTime != 0)
        {
            slide.fillAmount -= Time.deltaTime / itemTime;
            timeActive += Time.deltaTime;
        }
        if (isActivating && slide.fillAmount == 0)
        {
            slide.fillAmount = 1;
            isActivating = false;
            isPlayerCollect = false;
        }
    }

    protected override void ActiveActivity()
    {
        isActivating = true;
        DataController.Instance.UpdateItems((int)ItemKind.Weapon, -1);
        itemTime = item.itemTime;
        GameController.Instance.SetItemData(item);
        EventDisPatcher.Instance.PostEvent(EventID.OnUseWeapon);
        SetItemStatus();
    }
}
