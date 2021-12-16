using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCharacterParams : MonoBehaviour
{
    [SerializeField] private string characterTags;
    [SerializeField] private int characterID;
    [SerializeField] private GameObject MVP;
    [SerializeField] private Image characterFlag, characterAvatar, slideHP;
    //[SerializeField] private TextMeshProUGUI txtHP, txtGold;
    private CharacterParams characterParams;
    private float lastHp;
    private int lastGold;
    // Start is called before the first frame update
    void Start()
    {
        characterParams = GameObject.FindGameObjectsWithTag(characterTags)[characterID].GetComponent<CharacterParams>();
        if (characterTags == "Player") characterFlag.sprite = GameController.Instance.PlayerFlag;
        else characterFlag.sprite = GameController.Instance.GetRandomFlag();
        characterAvatar.sprite = characterParams.avatar;
        lastHp = characterParams.hp;
        lastGold = characterParams.gold;
        UpdateCharacterHP();
        //txtGold.SetText(lastGold.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if(lastHp != characterParams.hp)
        {
            lastHp = characterParams.hp;
            UpdateCharacterHP();
        }
        if (lastGold != characterParams.gold)
        {
            lastGold = characterParams.gold;
            //txtGold.SetText(lastGold.ToString());
        }
    }

    private void UpdateCharacterHP()
    {
        slideHP.fillAmount = (float)characterParams.hp / characterParams.maxHP;
        //txtHP.SetText((Mathf.CeilToInt(slideHP.fillAmount * 100)) + "%");
    }
}
