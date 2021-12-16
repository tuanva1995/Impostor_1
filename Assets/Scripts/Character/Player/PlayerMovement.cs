using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject joystickPointer;
    public float speedPointer;
    public float turnSpeed;
    private const float MULTIPLY_ANIM_SPEED = 2f;

    public List<GameObject> ListEnemy = new List<GameObject>();

    public WeaponAttack weaponScript;
    private CharacterParams characterParams;
    private PlayerControl playerControl;
    private Rigidbody rigidBody;
    private Animator anim;
    private Vector3 direction;
    private GameObject currentEnemy, nearestEnemy;
    private InitScenePlay InitPlay;
    public float moveSpeed;
    private float currentV, currentH;
    private float interpolation = 10;
    public Transform point;
    public bool isPointerUp;
    public ParticleSystem runPlay;
    // Start is called before the first frame update
    void Awake()
    {
        InitPlay = FindObjectOfType<InitScenePlay>();
        characterParams = GetComponent<CharacterParams>();
        weaponScript = GetComponent<WeaponAttack>();
        playerControl = GetComponent<PlayerControl>();
        rigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //isPointerUp = FindObjectOfType<UltimateJoystick>().isPointerUp;
        moveSpeed = characterParams.moveSpeed;
    }
    private void Start()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Bot");
        foreach (var enemy in enemies) ListEnemy.Add(enemy);
    }
   
    public bool isPlayerAttack = false;
    float attackTime = 0;
    public bool isPress;
    void AttackEnemy()
    {

    }
    private void Update()
    {
        if (InitPlay) if (!InitPlay.isStart) return;
        isPress = UltimateJoystick.isPointerDown;
        if (!isPlayerAttack&& isPress&&!playerControl.isOutMap)
            CalculateMovePlayer();
        //if (UltimateJoystick.isPointerUp)
        //{
        //    anim.SetFloat("moveSpeed", 0);
        //    UltimateJoystick.isPointerUp = false;
        //}
        if ( UltimateJoystick.isPointerUp )//(anim.GetFloat("moveSpeed") < 0.01f && !anim.GetBool("isStun") &&!isPlayerAttack)||
        {
            if (playerControl.isOutMap)
                return;
            UltimateJoystick.isPointerUp = false;
            isPlayerAttack = true;
            attackTime = 0;
            //weaponScript.rdWeapon = 0;
            isRun = false;
            weaponScript.SetAttack();
            runPlay.Stop();

        }
        //else weaponScript.StopAttack();
        if (isPlayerAttack)
        {
            if (attackTime < .5f)
            {
                attackTime += Time.deltaTime;
            }
            else
            {
                //anim.SetFloat("attackSpeed", 0);
                attackTime = 0;
                //isPlayerAttack = false;
                weaponScript.StopAttack();
            }
        }
        

    }
    public void AtackEvent()
    {
        if (weaponScript.rdWeapon == 2)
        {
            playerControl.gunMuzzle.Play();
        }
        UltimateJoystick.isPointerUp = false;
        var currentEnemys = GetEnemyAttack();
        // weaponScript.StopAttack();
        switch (weaponScript.rdWeapon)
        {
            case 0:
                SoundController.Instance.PlaySfx(SoundController.Instance.cutHorizontal);
                break;
            case 1:
                SoundController.Instance.PlaySfx(SoundController.Instance.knockDown);
                break;
            case 2:
                SoundController.Instance.PlaySfx(SoundController.Instance.shoot2);
                break;
            case 3:
                SoundController.Instance.PlaySfx(SoundController.Instance.bite);
                break;
            default:
                SoundController.Instance.PlaySfx(SoundController.Instance.broken);
                break;

        }
        if (currentEnemys!=null && currentEnemys.Count>0&&!GetComponent<PlayerControl>().isDead)
        {
            isPlayerAttack = true;
            foreach(GameObject go in currentEnemys)
            {
                go.GetComponent<BotControl>().TakenDamage(GetComponent<WeaponAttack>().rdWeapon);
                ListEnemy.Remove(go);
                GameController.Instance.playerLeft.text = ListEnemy.Count.ToString();
                //currentEnemy.GetComponent<BotControl>().SetBotDead();
                GameController.Instance.combo.SetCombo();
                var temp = new Vector3(playerControl.transform.localScale.x + 0.1f, playerControl.transform.localScale.y + 0.1f, playerControl.transform.localScale.z + .1f);
                moveSpeed = moveSpeed*1.1f;
                playerControl.transform.DOScale(temp, 1);
                FindObjectOfType<CameraController>().SetField();
                characterParams.attackDistance += .5f;
            }
          

        }

        

    }
    public void EndAnim()
    {
        weaponScript.lsWeapons[weaponScript.rdWeapon].SetActive(false);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //moveSpeed = playerControl.isSlow ? playerControl.slowSpeed : characterParams.moveSpeed;
        isPress = UltimateJoystick.isPointerDown;
        if (isPlayerAttack|| anim.GetFloat("moveSpeed")<0.1f)
        {
            rigidBody.velocity = Vector3.zero;
        }
        else if (isPress)
        {
            //Debug.Log(isPress);
            rigidBody.velocity = new Vector3(currentH, 0f, currentV) * moveSpeed;
        }
           
    }
    bool isRun = false;
    void CalculateMovePlayer()
    {
        float v = UltimateJoystick.GetVerticalAxis("Movement");
        float h = UltimateJoystick.GetHorizontalAxis("Movement");
        currentV = Mathf.Lerp(currentV, v, Time.deltaTime * interpolation);
        currentH = Mathf.Lerp(currentH, h, Time.deltaTime * interpolation);
        direction = Vector3.forward * currentV + Vector3.right * currentH;
        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;
        SetPosJoystickPointer(direction);
        //Debug.Log("moveSpeed "+ direction);
        if (direction != Vector3.zero && !anim.GetBool("isStun")&&!isPlayerAttack)
        {
            float _moveSpeed = Mathf.Clamp(direction.magnitude * MULTIPLY_ANIM_SPEED, 0, 1);
            if(_moveSpeed > 0.01f)
            {
                float atan2 = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, (atan2), 0), 20 * Time.deltaTime);
            }
                //transform.rotation = Quaternion.LookRotation(direction);
           
            anim.SetFloat("moveSpeed", 1);
            if (!isRun)
            {
                isRun = true;
                runPlay.Play();
            }
           

        }
        
       
    }
    private void OnDisable()
    {
        currentV = currentH = 0f;
    }
    void SetPosJoystickPointer(Vector3 direction)
    {
        Vector3 pointerPos = transform.position + direction * speedPointer;
        if ((direction * speedPointer).magnitude < 0.6f) return;
        joystickPointer.transform.position = new Vector3(pointerPos.x, joystickPointer.transform.position.y, pointerPos.z);
    }

    List<GameObject> GetEnemyAttack()
    {
        List<GameObject> nearestAttackEnemy = new List<GameObject>();
        float nearestAttackDistance = 0;
        //float nearestDistance = 0;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
        foreach (var enemy in ListEnemy)
        {
            if (enemy.activeInHierarchy)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                //if(nearestDistance == 0 || distance < nearestDistance)
                //{
                //    nearestDistance = distance;
                //    nearestEnemy = enemy;
                //}
                if (distance < characterParams.attackDistance && enemy.GetComponent<CharacterParams>().hp != 0)
                {
                    Vector3 vec = transform.position;
                    vec.y = transform.position.y + 0.1f;
                    Ray ray = new Ray(vec, (enemy.transform.position - transform.position).normalized);
                    Debug.DrawRay(vec, enemy.transform.position - transform.position, Color.red);
                    RaycastHit hit;
                    int layerMask = ~LayerMask.GetMask("Ignore Raycast"); 
                    
                    //if (Physics.Raycast(ray, out hit, 5f, layerMask))
                    {
                        //Debug.Log(hit.collider.name);
                        //if (hit.collider.CompareTag(enemy.tag))
                        {
                            Vector3 direction = new Vector3(enemy.transform.position.x - transform.position.x, transform.position.y, enemy.transform.position.z - transform.position.z);
                            float dis = Vector3.Distance(enemy.transform.position, transform.position);
                            //float AngleRad = Mathf.Atan2(direction.x - direction1.x, direction.z - direction1.z);
                            // Get Angle in Degrees
                            // float AngleDeg = AngleInDeg(direction, direction1);
                            Vector3 pro = Vector3.ProjectOnPlane(direction, transform.up);
                            
                            Vector2 a = new Vector2(pro.x, pro.z);
                            Vector2 b = new Vector2(transform.forward.x, transform.forward.z);
                            float AngleDeg = Vector3.Angle(a, b);
                            //Debug.Log("Dir : "+ pro + "--- "+ AngleDeg);
                            if (((AngleDeg > 0&& AngleDeg < 30)|| (AngleDeg >-30 && AngleDeg < 0)))
                            {
                                //Debug.Log(angle);
                                //nearestAttackDistance = distance;
                                nearestAttackEnemy.Add(enemy);
                            }
                        }
                    }
                }

            }
        }
        return nearestAttackEnemy;
    }
    public static float AngleInRad(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec2.z - vec1.z, vec2.x - vec1.x);
    }

    //This returns the angle in degrees
    public static float AngleInDeg(Vector3 vec1, Vector3 vec2)
    {
        return AngleInRad(vec1, vec2) * 180 / Mathf.PI;
    }
    //bool IsDetectedPlayer()
    //{
    //    Vector3 direction = new Vector3(Player.transform.position.x - transform.position.x, transform.position.y, Player.transform.position.z - transform.position.z);
    //    float angle = Vector3.Angle(transform.forward, direction);
    //    Vector3 vec = transform.position;
    //    vec.y = transform.position.y + 0.1f;

    //    Ray ray = new Ray(vec, (Player.transform.position - transform.position).normalized);
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit, raycastDistance))
    //    {
    //        if (hit.collider.CompareTag(Player.tag) && angle < maxViewAngle) return true;
    //    }
    //    return false;
    //}
}
