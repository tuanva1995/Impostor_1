using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening;
public interface ITakenDamage
{
    void TakenDamage(float damage);
}

public interface IStepOnTrap
{
    void StepOnBless(TrapData trap);
    void StepOutBless(TrapData trap);
    void StepOnSlow(TrapData trap);
    void StepOutSlow(TrapData trap);
    void StepOnStun(TrapData trap, float timeStun);
}

public interface ICollectGold
{
    void CollectGold();
}

public class PlayerControl : MonoBehaviour, ITakenDamage, ICollectGold, IStepOnTrap
{
    [SerializeField] private SkinnedMeshRenderer playerMesh;
    [SerializeField] public ParticleSystem StunEffect, PowerPoleEffect, SawEffect, HitDmgEffect,hitGun, CoinEffect, SkullBallEffect,gunMuzzle,win1Effect, win2Effect , effectDie;

    [HideInInspector]
    public float slowSpeed;
    [HideInInspector]
    public bool isSlow;

    private CharacterParams characterParams;
    public bool isReviving,isOutMap;

    private TrapKind currentOnTrap;
    private float timeOnTrap;
    private bool iscooldownTrap;
    private bool canStun = true;
    private Vector3 oriPos;

    private PlayerUseItem useItem;
    private PlayerMovement moveScript;
    private WeaponAttack weaponScript;
    private Rigidbody rigid;
    public Animator anim;

    public Texture TakenDmgImg;
    private Texture currentImg;
    public Transform point;
    public GameObject[] deaths;
    public GameObject playerGo;
    public bool isDead = false, isShield = false;
    private void Awake()
    {
        moveScript = GetComponent<PlayerMovement>();
        weaponScript = GetComponent<WeaponAttack>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        characterParams = GetComponent<CharacterParams>();
        useItem = GetComponent<PlayerUseItem>();
    }

    void Start()
    {
        if(SceneManager.GetActiveScene().name != "TestSkinScene")
            SetSkinPlayer(DataController.Instance.playerData.playerCurrentIndex);
        oriPos = transform.position;
        SawEffect.Play();
        isShield = true;
        Invoke("StartShield", 1);
    }
    void StartShield()
    {
        characterParams.hp = 1;
        isShield = false;
        SawEffect.Stop();
        
    }
    void DelayShield()
    {
        isShield = false;
    }
    public void SetSkinPlayer(int skinID)
    {
        //playerMesh.material.mainTexture = GameController.Instance.GetPlayerSkin(skinID);
       // currentImg = playerMesh.material.mainTexture;
       // GetComponent<CharacterParams>().avatar = GameController.Instance.GetPlayerAvatar(skinID);
    }
    public void TakenDamage(float damage)
    {
        if (isOutMap)
            return;
        Debug.Log("TakenDamage " + damage);
        //MinusHealth(damage);
        SetPlayerDead((int)damage);
        if (weaponScript.rdWeapon != 2)
            HitDmgEffect.Play();
        else
            hitGun.Play();
        // playerMesh.material.mainTexture = TakenDmgImg;
        //MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxAtk1);
        //TweenControl.GetInstance().DelayCall(transform, 0.1f, () => { 
        //    //playerMesh.material.mainTexture = currentImg; 
        //    //anim.SetTrigger("isTakenDmg");
           
        //});
    }

    public void CollectGold()
    {
        characterParams.gold += 10;
        if (!CoinEffect.isPlaying) CoinEffect.Play();
    }

    public void Revival()
    {
        
        isReviving = true;
        anim.SetBool("isDead", false);
        isDead = false;
        characterParams.isDead = false;
        for (int i = 0; i < deaths.Length; i++)
        {
            deaths[i].SetActive(false);
        }
        playerGo.SetActive(true);
        moveScript.enabled = true;
        //GetComponent<Collider>().isTrigger = true;
        GameController.Instance.isFinish = false;
       
        // characterParams.UpdateHeartBar();
        rigid.constraints = RigidbodyConstraints.None;
        GetComponent<Collider>().isTrigger = true;
        GetComponent<NavMeshAgent>().enabled = true;
        moveScript.enabled = true;
        
        isOutMap = false;
        SetShield();
    }

    void SetShield()
    {
        SawEffect.Play();
        Invoke("StartShield",2);
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    Debug.Log("ATKKKKKKK "+ other.gameObject.tag);
    //    if (other.gameObject.tag == "OutMap"&& !isOutMap)
    //    {
           
           
            
    //       // SetPlayerDead(1);
    //    }
    //}
    float oriY;
    public bool isStartTele = true;
    void StartTele()
    {
        isStartTele = true;
    }
    public void SetTelePort(Vector3 ori,int index)
    {
        oriY = transform.position.y;
        var targetPoint = FindObjectOfType<MapPoint>().GetPos(index);
        anim.SetFloat("attackSpeed", 0f);
        transform.DOMoveY(oriY - 4, 3).SetSpeedBased(true).OnComplete(()=> {
            transform.position = new Vector3(targetPoint.x+.4f, oriY-4, targetPoint.z+.4f);
            // var cam = FindObjectOfType<CameraFollow>();
            //var pos = ori - cam.transform.position;
            // cam.transform.DOMove(pos+targetPoint, .5f);
            isOutMap = false;
            anim.SetFloat("moveSpeed", 0f);
            
            transform.DOMoveY(oriY, 3).SetSpeedBased(true).OnComplete(()=> {
               // SawEffect.Play();
                Invoke("StartShield", 1);
                Invoke("StartTele", 1);
            });
        });
    }
    private void Update()
    {
        if(iscooldownTrap)
            timeOnTrap += Time.deltaTime;
       
    }
    public void StepOnBless(TrapData trap)
    {
        if(currentOnTrap == trap.kindTrap)
        {
            timeOnTrap += Time.deltaTime;
            iscooldownTrap = true;
            if(timeOnTrap > trap.cooldownTime)
            {
                timeOnTrap = 0;
                iscooldownTrap = false;
                playerMesh.material.mainTexture = TakenDmgImg;
                if (trap.kindTrap == TrapKind.PowerPole)
                {
                    PowerPoleEffect.Play();
                    //MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxElectric);
                }
                else if (trap.kindTrap == TrapKind.Saw) SawEffect.Play();
                else if (trap.kindTrap == TrapKind.SkullBall)
                {
                    //MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxBoom);
                    SkullBallEffect.Play();
                }
                TweenControl.GetInstance().DelayCall(transform, 0.1f, () => {
                    playerMesh.material.mainTexture = currentImg;
                    MinusHealth(trap.damageValue);
                });
            }
        }
        else
        {
            currentOnTrap = trap.kindTrap;
            timeOnTrap = 0;
            playerMesh.material.mainTexture = TakenDmgImg;
            if (trap.kindTrap == TrapKind.PowerPole)
            {
                PowerPoleEffect.Play();
                //MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxElectric);
            }
            else if (trap.kindTrap == TrapKind.Saw) SawEffect.Play();
            else if (trap.kindTrap == TrapKind.SkullBall) SkullBallEffect.Play();

            TweenControl.GetInstance().DelayCall(transform, 0.1f, () => {
                playerMesh.material.mainTexture = currentImg;
                MinusHealth(trap.damageValue);
            });
        }
    }

    public void StepOutBless(TrapData trap)
    {
        if (currentOnTrap == trap.kindTrap)
        {
            timeOnTrap = 0;
            currentOnTrap = TrapKind.None;
        }
    }

    public void StepOnSlow(TrapData trap)
    {
        slowSpeed = characterParams.moveSpeed / trap.damageValue;
        isSlow = true;
    }

    public void StepOutSlow(TrapData trap)
    {
        isSlow = false;
    }

    public void StepOnStun(TrapData trap, float timeStun)
    {
        if (!canStun || isReviving) return;

        StepOnBless(trap);
        canStun = false;
        anim.SetBool("isStun", true);
        anim.SetFloat("moveSpeed", 0f);
        StunEffect.Play();
        //MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxStun);
        TweenControl.GetInstance().DelayCall(transform, timeStun, () =>
        {
            anim.SetBool("isStun", false);
            StunEffect.Stop();
            TweenControl.GetInstance().DelayCall(transform, trap.cooldownTime, () => {
                canStun = true; });
        });
    }

    private void SetPlayerDead(int index)
    {
        if (isShield)
            return;
        isDead = true;
        moveScript.runPlay.Stop();
        deaths[index].SetActive(true);
        //MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxPlayerDie);
        weaponScript.StopAttack();
        if(!isOutMap)
        {
            //anim.SetBool("isDead", true);
            rigid.constraints = RigidbodyConstraints.FreezeAll;
        }
        SoundController.Instance.PlaySfx(SoundController.Instance.playerDie);
        playerGo.SetActive(false);
        moveScript.enabled = false;
        characterParams.hp = 0;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Collider>().isTrigger = true;
        GameController.Instance.isFinish = true;
        float timeDead = isOutMap ? 1f : 3f;
        characterParams.isDead = true;
        //gameObject.SetActive(false);
        TweenControl.GetInstance().DelayCall(transform, .1f, () => {
            if(isOutMap) anim.SetBool("isDead", true);
            if (!GUIManager.IsGUIActive(GUIName.VictoryDialog)) {
                //MusicManager.Instance.PlaySfx(MusicManager.Instance.sfxLose);
                if(!isReviving)
                    GUIManager.ShowGUI(GUIName.YouDieDialog);
                else
                    GUIManager.ShowGUI(GUIName.DefeatDialog);
            }
            var StormTrap = CustomUtils.GameObjectUtils.FindObjectsOfTypeAll<StormDomeTrap>();
            if (StormTrap != null)
            {
                foreach (var storm in StormTrap)
                {
                    storm.gameObject.SetActive(false);
                }
            }
           
        });

        if (effectDie != null)
            effectDie.Play();
    }
    private void MinusHealth(float dmg)
    {
        //if (!isReviving && !useItem.haveShield)
        {
            characterParams.hp -= dmg;
            if (characterParams.hp < 0)
                characterParams.hp = 0;
        }
        characterParams.UpdateHeartBar();
        if (characterParams.hp == 0 && !anim.GetBool("isDead"))
            SetPlayerDead(1);
    }
    /* private void OnCollisionEnter(Collision collision)
     {
         Debug.Log("collision:" + collision.collider.name);
         if (collision.collider.tag == "Coin")
         {
             Debug.Log("=========== eat coin");
             collision.gameObject.SetActive(false);


         }
     }*/

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("========= collision.rigidbody.tag: " + collision.other.tag);
        if (collision.other.tag == "Coin")
        {
            Destroy(collision.gameObject);

        }
        //ContactPoint contact = collision.contacts[0];
        //Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        //Vector3 position = contact.point;
    }




}
