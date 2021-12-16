using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseItem : MonoBehaviour
{
    [SerializeField] private ParticleSystem HealEffect, ShieldEffect, StrengthEffect;
    [HideInInspector]
    public bool haveShield, haveStrength;

    private CharacterParams characterParams;
    private ItemData shieldItem, strengthItem;
    private float curShieldTime, shieldItemTime;
    private float curStrengthTime, strengthItemTime;
    private Action<object> OnUseHealRef, OnUseShieldRef, OnUseStrengthRef;

    void Awake()
    {
        //characterParams = GetComponent<CharacterParams>();

        //shieldItem = GameController.Instance.GetItemData(ItemKind.Shield);
        //strengthItem = GameController.Instance.GetItemData(ItemKind.Strength);

        //OnUseHealRef = (obj) => UseHealItem();
        //OnUseShieldRef = (obj) => UseShieldItem();
        //OnUseStrengthRef = (obj) => UseStrengthItem();
    }

    private void OnEnable()
    {
        //EventDisPatcher.Instance.RegisterListener(EventID.OnUseHeal, OnUseHealRef);
        //EventDisPatcher.Instance.RegisterListener(EventID.OnUseShield, OnUseShieldRef);
        //EventDisPatcher.Instance.RegisterListener(EventID.OnUseStrength, OnUseStrengthRef);
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateApplyShield();
        //UpdateApplyStrength();
    }

    private void UseHealItem()
    {
        HealEffect.Play();
        characterParams.hp += (int)GameController.Instance.GetItemData(ItemKind.Heal).itemValue;
        characterParams.hp = Mathf.Clamp(characterParams.hp, 0, characterParams.maxHP);
        characterParams.UpdateHeartBar();
    }

    public void UseShieldItem()
    {
        shieldItemTime += shieldItem.itemTime;
        haveShield = true;
    }

    void UseStrengthItem()
    {
        StrengthEffect.Play();
        strengthItemTime += strengthItem.itemTime;
        characterParams.damage += strengthItem.itemValue;
        haveStrength = true;
    }

    void UpdateApplyShield()
    {
        if (haveShield)
        {
            if (curShieldTime < shieldItemTime)
                curShieldTime += Time.deltaTime;
            else
            {

                curShieldTime = 0;
                shieldItemTime = 0;
                haveShield = false;
            }
        }
    }
    void UpdateApplyStrength()
    {
        if (haveStrength)
        {
            if (curStrengthTime < strengthItemTime)
                curStrengthTime += Time.deltaTime;
            else
            {
                characterParams.damage -= strengthItem.itemValue;
                StrengthEffect.Stop();
                curStrengthTime = 0;
                strengthItemTime = 0;
                haveStrength = false;
            }
        }
    }

    private void OnDisable()
    {
        if (EventDisPatcher.Instance != null)
        {
            EventDisPatcher.Instance.RemoveListener(EventID.OnUseHeal, OnUseHealRef);
            EventDisPatcher.Instance.RemoveListener(EventID.OnUseShield, OnUseShieldRef);
            EventDisPatcher.Instance.RemoveListener(EventID.OnUseStrength, OnUseStrengthRef);
        }
    }

}
