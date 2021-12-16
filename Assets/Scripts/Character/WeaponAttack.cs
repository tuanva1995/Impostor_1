using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeWeapon
{
    void TakeWeapon(GameObject weapon, Vector3 localPos, Quaternion localRot, Vector3 localScale);
    bool HaveWeapon();
}

public class WeaponAttack : MonoBehaviour, ITakeWeapon
{
    [SerializeField] private Transform WeaponHand;
    public bool isWeaponPlayer;
    [SerializeField] GameObject transGuns;
    [SerializeField] GameObject[] arrayGun;
    [SerializeField] GameObject[] arraySwordHorizontal;
    [SerializeField] GameObject[] arraySwordVertical;

    private Animator characterAnim;
    private CharacterParams characterParams;
    private ITakenDamage TakeDmgEnemy;
    private ItemData weaponData;
    private float curWeaponTime, weaponItemTime;
    public GameObject[] lsWeapons;
    // Start is called before the first frame update
    public int rdWeapon = 0;
    public int idWeaponInArr;
    void Awake()
    {
        characterAnim = GetComponentInParent<Animator>();
        characterParams = GetComponentInParent<CharacterParams>();
        weaponData = GameController.Instance.GetItemData(ItemKind.Weapon);
        weaponItemTime = weaponData.itemTime;
        SetWeapon();
    }
    void SetWeapon()
    {
        
        rdWeapon = Random.Range(0, 4);
        
        if (rdWeapon == 0)
        {
            idWeaponInArr = UnityEngine.Random.Range(0, arraySwordHorizontal.Length);
            for(int i=0; i< arraySwordHorizontal.Length; i++)
            {
                arraySwordHorizontal[i].GetComponent<MeshRenderer>().enabled = (i == idWeaponInArr);
            }
        }else if(rdWeapon == 1)
        {
            idWeaponInArr = UnityEngine.Random.Range(0, arraySwordVertical.Length);
            for (int i = 0; i < arraySwordVertical.Length; i++)
            {
                arraySwordVertical[i].GetComponent<MeshRenderer>().enabled = (i == idWeaponInArr);
            }

        }
        else
        {
            idWeaponInArr = UnityEngine.Random.Range(0, arrayGun.Length);
            for (int i = 0; i < arrayGun.Length; i++)
            {
                arrayGun[i].GetComponent<MeshRenderer>().enabled = (i == idWeaponInArr);
            }
        }

    }
    void Start()
    {
        characterAnim = GetComponentInParent<Animator>();
    }

    private void OnEnable()
    {
        //var children = WeaponHand.GetComponentsInChildren<Transform>();
        //foreach (var child in children) if(child.CompareTag("Weapon")) characterAnim.SetBool("haveWeapon", true);
    }
    private void Update()
    {
        UpdateApplyWeapon();
    }

    public void AttackEnemyAnimEvent()
    {
        //Debug.Log(gameObject.name + " Attack");
        if (TakeDmgEnemy != null)
        {
            TakeDmgEnemy.TakenDamage(characterParams.damage);
        } 
    }
    public void RemoveWeapon()
    {
        //Debug.Log("Remove item");
        var children = WeaponHand.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.CompareTag("Weapon"))
            {
                Destroy(child.gameObject);
                if(characterAnim) characterAnim.SetBool("haveWeapon", false);
                return;
            }
        }
    }
    public void TakeWeapon(GameObject weapon, Vector3 localPos, Quaternion localRot, Vector3 localScale)
    {
        weapon.transform.SetParent(WeaponHand);
        weapon.transform.localPosition = localPos;
        weapon.transform.localRotation = localRot;
        weapon.transform.localScale = localScale;
        characterAnim.SetBool("haveWeapon", true);
        characterParams.damage += (int)weaponData.itemValue;
    }

    void UpdateApplyWeapon()
    {
        if (characterAnim.GetBool("haveWeapon"))
        {
            if (curWeaponTime < weaponItemTime)
                curWeaponTime += Time.deltaTime;
            else
            {
                curWeaponTime = 0;
                RemoveWeapon();
            }
        }
    }

    public bool HaveWeapon()
    {
        return characterAnim.GetBool("haveWeapon");
    }
    public void SetAttack(GameObject enemy)
    {
        if (GameController.Instance.isFinish)
        {
            StopAttack();
            return;
        }

       
        var takedmg = enemy.GetComponent<ITakenDamage>();
        if (takedmg != null) TakeDmgEnemy = takedmg;
    }
    public void SetAttack()
    {
        //if (GameController.Instance.isFinish)
        //{
        //    StopAttack();
        //    return;
        //}
        //if (rdWeapon < 1)
        //{
        //    characterAnim.SetBool("haveWeapon", false);
        //}
        //else
        //{
        //    characterAnim.SetBool("haveWeapon", true);
        //}
        if(rdWeapon<3)
            lsWeapons[rdWeapon].SetActive(true);
       // lsWeapons[rdWeapon].GetComponent<Animator>().SetBool("isAttack", true);
        characterAnim.SetInteger("WeaponIndex", rdWeapon);
        characterAnim.SetFloat("moveSpeed", 0);
        characterAnim.SetBool("isAttack", true);
        characterAnim.SetFloat("attackSpeed", characterParams.attackSpeed);

        
        //var takedmg = enemy.GetComponent<ITakenDamage>();
        //if (takedmg != null) TakeDmgEnemy = takedmg;
    }
    public void StopAttack()
    {
        var player = GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.isPlayerAttack = false;
        }
        else
        {
            GetComponent<BotMovement>().isBotAttack = false;
        }
        if (rdWeapon < 3)
            lsWeapons[rdWeapon].SetActive(false);
       // lsWeapons[rdWeapon].GetComponent<Animator>().SetBool("isAttack", false);
        characterAnim.SetFloat("attackSpeed", 0);
        characterAnim.SetFloat("moveSpeed", 0);
        characterAnim.SetBool("isAttack", false);
        //Invoke("Delay", 0.5f);
    }
    void Delay()
    {
        var player = GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.isPlayerAttack = false;
        }
    }
}
