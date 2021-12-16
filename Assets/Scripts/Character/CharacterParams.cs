using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class CharacterParams : MonoBehaviour
{
    [SerializeField] private GameConfigSO characterData;
    [SerializeField] private Transform heartBar;
    public abstract Kind_Character kindCharacter { get; }
    public Sprite avatar;
    public float hp;
    public float damage;
    public float attackSpeed;
    public float moveSpeed;
    public float attackDistance;
    public float maxHP;
    public int gold;
    public bool isDead = false;
    private CharacterParameter oriParams;
    private Vector3 oriPos;

    private void Awake()
    {
        oriPos = transform.position;
        foreach (var character in characterData.characterParamArray)
        {
            if(kindCharacter == character.kindCharacter)
            {
                oriParams = character;
            }
        }
        hp = oriParams.oriHP;
        maxHP = oriParams.oriMaxHP;
        damage = oriParams.oriDamage;
        attackSpeed = oriParams.oriAttackSpeed;
        attackDistance = oriParams.oriAttackDistance;
        moveSpeed = oriParams.oriMoveSpeed;
        CustomSetParams();
    }

    public void ResetParams()
    {
        hp = oriParams.oriHP;
        maxHP = oriParams.oriMaxHP;
        damage = oriParams.oriDamage;
        attackSpeed = oriParams.oriAttackSpeed;
        attackDistance = oriParams.oriAttackDistance;
        moveSpeed = oriParams.oriMoveSpeed;
        CustomSetParams();

        GetComponent<Animator>().SetBool("isDead", false);
        GetComponent<WeaponAttack>().RemoveWeapon();
        gameObject.SetActive(true);
        transform.position = oriPos;
    }

    public void UpdateHeartBar()
    {
        var scale = heartBar.transform.localScale;
        scale.x = hp / maxHP;
        heartBar.transform.localScale = scale;
    }

    protected virtual void CustomSetParams()
    {

    }
}
