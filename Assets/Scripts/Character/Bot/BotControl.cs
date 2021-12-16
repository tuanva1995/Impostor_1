using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;

public class BotControl : MonoBehaviour, ITakenDamage, ICollectGold, IStepOnTrap
{
    public int botID;
    [SerializeField] private BotDataSO botDatas;
    [SerializeField] private ParticleSystem StunEffect, PowerPoleEffect, SawEffect, HitDmgEffect, SkullBallEffect, effectDie;
    [SerializeField] private GameObject Coin;
    [SerializeField] private Transform heartBar;
    public bool isOnSfx;
    private CharacterParams characterParams;
    private Animator anim;
    private WeaponAttack weaponScript;
    private BotMovement botMove;
    private SkinnedMeshRenderer skinMesh;

    private TrapKind currentOnTrap;
    private float timeOnTrap;
    private bool iscooldownTrap;

    [HideInInspector]
    public float slowSpeed;
    [HideInInspector]
    public bool isSlow;

    public Material takenDmgMaterial;
    private Material currentMaterial;
    public GameObject playerGo,transGo;
    public GameObject[] deaths;
    private bool canStun = true;
    public bool isDead = false;
    Transform playerTrans;
    private void Awake()
    {
        playerTrans = FindObjectOfType<PlayerMovement>().transform;
        characterParams = GetComponent<CharacterParams>();
        weaponScript = GetComponent<WeaponAttack>();
        anim = GetComponent<Animator>();
        botMove = GetComponent<BotMovement>();
        skinMesh = GetComponentInChildren<SkinnedMeshRenderer>();
        //skinMesh.material = botDatas.botMaterials[botID];
        //characterParams.avatar = botDatas.botAvatars[botID];

        //currentMaterial = skinMesh.material;
    }
    void StartShield()
    {
        SawEffect.Stop();
    }
    private void OnEnable()
    {
        botMove.enabled = true;
    }
    void Start()
    {
        SawEffect.Play();
        Invoke("StartShield", 1);
    }
    public void TakenDamage(float damage)
    {
        //skinMesh.material = takenDmgMaterial;
        MinusHealth(damage);
        HitDmgEffect.Play();
      
        //TweenControl.GetInstance().DelayCall(transform, 0f, () => {
        //   // skinMesh.material = currentMaterial;
        //   // anim.SetTrigger("isTakenDmg");
        //    EventDisPatcher.Instance.PostEvent(EventID.OnDetectPlayer);
           
        //});
    }

    public void UpdateSkinBot()
    {
        //skinMesh.material = botDatas.botMaterials[botID];
        //characterParams.avatar = botDatas.botAvatars[botID];
        //currentMaterial = skinMesh.material;
    }

    private void Update()
    {
        if (iscooldownTrap)
            timeOnTrap += Time.deltaTime;
        //if (isDead)
        //{
        //    Quaternion rotTarget = Quaternion.LookRotation(characterParams.transform.position - transform.position);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, Time.deltaTime * 700);
        //}
    }        

    public void StepOnBless(TrapData trap)
    {
        if (currentOnTrap == trap.kindTrap)
        {
            timeOnTrap += Time.deltaTime;
            iscooldownTrap = true;
            if (timeOnTrap > trap.cooldownTime)
            {
                timeOnTrap = 0;
                iscooldownTrap = false;
                skinMesh.material = takenDmgMaterial;
                if (trap.kindTrap == TrapKind.PowerPole)
                {
                    PowerPoleEffect.Play();
                 
                }
                else if (trap.kindTrap == TrapKind.Saw) SawEffect.Play();
                else if (trap.kindTrap == TrapKind.SkullBall)
                {
                    SkullBallEffect.Play();
                    
                }

                TweenControl.GetInstance().DelayCall(transform, 0.1f, () => {
                    skinMesh.material = currentMaterial;
                    MinusHealth(trap.damageValue);
                });
            }
        }
        else
        {
            currentOnTrap = trap.kindTrap;
            timeOnTrap = 0;
            skinMesh.material = takenDmgMaterial;
            if (trap.kindTrap == TrapKind.PowerPole)
            {
                PowerPoleEffect.Play();
                
            }
            else if (trap.kindTrap == TrapKind.Saw) SawEffect.Play();
            else if (trap.kindTrap == TrapKind.SkullBall)
            {
                SkullBallEffect.Play();
               
            }

            TweenControl.GetInstance().DelayCall(transform, 0.1f, () => {
                skinMesh.material = currentMaterial;
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
        if (!canStun) return;
        StepOnBless(trap);
        canStun = false;
        anim.SetBool("isStun", true);
        //anim.SetFloat("moveSpeed", 0f);
        StunEffect.Play();
       
        TweenControl.GetInstance().DelayCall(transform, timeStun, () =>
        {
            anim.SetBool("isStun", false);
            anim.SetFloat("moveSpeed", 1f);
            StunEffect.Stop();
            TweenControl.GetInstance().DelayCall(transform, trap.cooldownTime, () => { canStun = true; });
        });
    }

    private void MinusHealth(float dmg)
    {
        //characterParams.hp -= dmg;
        //if (characterParams.hp < 0)
        //    characterParams.hp = 0;
        //characterParams.UpdateHeartBar();
        //if (characterParams.hp == 0 && !anim.GetBool("isDead"))
        //    SetBotDead();
        SetBotDead((int)dmg);
    }

    public void SetBotDead(int index)
    {
        weaponScript.StopAttack();
        anim.SetBool("isDead", true);
        botMove.enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CharacterParams>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        //gameObject.tag = "";
        //SpawnGoldWhenDie();
        playerGo.SetActive(false);
        transGo.SetActive(false);
        isDead = true;
        GetComponent<CharacterParams>().isDead = true;
        transform.LookAt(playerTrans);
        deaths[index].SetActive(true);
        SoundController.Instance.PlaySfx(SoundController.Instance.enemyDie);
        if (IsEndGame())
        {
            if (!GUIManager.IsGUIActive(GUIName.YouDieDialog))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("win");
            }
            GameController.Instance.isFinish = true;
            UltimateJoystick.DisableJoystick("Movement");
            TweenControl.GetInstance().DelayCall(transform, 3f, () => {
                if (!GUIManager.IsGUIActive(GUIName.YouDieDialog))
                {
                    GUIManager.ShowGUI(GUIName.VictoryDialog);
                
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
        }
        GetComponent<Target>().enabled = false;
        if(effectDie != null)
        effectDie.Play();
        //DataController.Instance.UpdateMisson(0, 1);
    }

    private void SpawnGoldWhenDie()
    {
        var Player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < 10; i++)
        {
            var coin = Instantiate(Coin, transform.position, Coin.transform.rotation);
            float defCoinPosY = coin.transform.position.y;
            Vector3 randDirection = Random.insideUnitSphere * 2f;
            randDirection += coin.transform.position;
            randDirection.y = defCoinPosY;
            TweenControl.GetInstance().Move(coin.transform, randDirection, 0.3f, () =>
            {
                Destroy(coin.GetComponent<NavMeshAgent>());
                TweenControl.GetInstance().DelayCall(transform, 0.3f, () =>
                {
                    TweenControl.GetInstance().Move(coin.transform, Player.transform.position, 0.2f, () =>
                    {
                        Player.GetComponent<PlayerControl>().CollectGold();
                        Destroy(coin);
                        gameObject.SetActive(false);
                    });
                });
            });

        }
    }
    private bool IsEndGame()
    {
        bool isEnd = true;
        var players = playerTrans.GetComponent<PlayerMovement>();
        if (players.ListEnemy.Count ==1)
            return isEnd;
        else
            return false;
    }
    public void CollectGold()
    {
        characterParams.gold += 10;
    }
}
