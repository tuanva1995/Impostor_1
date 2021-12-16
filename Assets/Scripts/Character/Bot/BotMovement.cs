using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using DG.Tweening;

public class BotMovement : MonoBehaviour
{
    [SerializeField] private float wanderRadius;
    [SerializeField] private float raycastDistance = 5f;
    [SerializeField] private float detectedDistance = 12f;
    [SerializeField] private float multiplyDetectSpeed = 1.5f;
    [SerializeField] private float maxViewAngle = 60f;
    [SerializeField] private float DetectedWidth = 0.1f;
    [SerializeField] private float timeToFollow = 3f;
    [SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private float turnAttackSpeed;

    [SerializeField] private Transform raycastTarget;
    
    private const float MIN_VELOCITY = 0.1f;

    private NavMeshAgent agent;
    private Animator anim;
    private NavMeshPath path;
    private NavMeshHit navHit;
    private GameObject Player;
    private CharacterParams characterParams, playerParams;
    private WeaponAttack attackScript;
    private BotControl botControl;
    private InitScenePlay InitPlay;
    private Action<object> OnDetectPlayerRef;

    public bool isFollowingPlayer, isDetectedPlayer;
    public List<GameObject> ListEnemy = new List<GameObject>();
    private void Awake()
    {
        InitPlay = FindObjectOfType<InitScenePlay>();

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        path = new NavMeshPath();
        characterParams = GetComponent<CharacterParams>();
        attackScript = GetComponent<WeaponAttack>();
        botControl = GetComponent<BotControl>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerParams = Player.GetComponent<CharacterParams>();
        OnDetectPlayerRef = (obj) => DetectPlayer();
        
    }
    private void Start()
    {
        agent.speed = characterParams.moveSpeed;
        ListEnemy = FindObjectOfType<InitLevelData>().lsEnemys;
        //for(int i = 0; i < ListEnemy.Count; i++)
        //{

        //    if (ListEnemy[i].GetComponent<BotControl>() != null&& ListEnemy[i].GetComponent<BotControl>().botID == GetComponent<BotControl>().botID)
        //    {
        //        ListEnemy.RemoveAt(i);
        //        break;
        //    }
        //}
    }
    private void OnEnable()
    {
        EventDisPatcher.Instance.RegisterListener(EventID.OnDetectPlayer, OnDetectPlayerRef);
    }
    // Update is called once per frame
    float attackTime = 0;
    void Update()
    {
        if(InitPlay) if (!InitPlay.isStart) return;

        //if (anim.GetBool("isStun"))
        //{
        //    agent.speed = 0f;
        //    attackScript.StopAttack();
        //    return;
        //}
        var enemys = GetEnemyAttack();
        if (enemys!=null&&enemys.Count>0 && !isBotAttack)
        {
           
            agent.speed = 0f;
            attackScript.SetAttack();
            isBotAttack = true;
            switch (attackScript.rdWeapon)
            {
                case 0:
                    SoundController.Instance.PlaySfx(SoundController.Instance.cutHorizontal);
                    break;
                case 1:
                    SoundController.Instance.PlaySfx(SoundController.Instance.cutVertical);
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
            {
                
                //anim.SetFloat("moveSpeed", 0);
                ////characterAnim.SetFloat("attackSpeed", 0);
                //anim.SetFloat("attackSpeed", characterParams.attackSpeed);
                //Player.GetComponent<PlayerControl>().TakenDamage(1000);

                
            }
            return;
        }
        else if(!isBotAttack)
        {
            if (IsCanMove())
            {
                agent.speed = botControl.isSlow ? botControl.slowSpeed : characterParams.moveSpeed;
                anim.SetFloat("moveSpeed", 1);
            }
            else
                attackScript.StopAttack();
            if (playerParams.hp == 0)
            {
                isFollowingPlayer = false;
                MoveRandomNav(agent.destination, wanderRadius, -1);
                return;
            }
        }
        DetectPlayer();
        //isDetectedPlayer = IsDetectedPlayer();
        //if (isDetectedPlayer)
        //{
        //    EventDisPatcher.Instance.PostEvent(EventID.OnDetectPlayer);
        //}
        if (!isBotAttack)
            RandomMove();
        else
        {
            if (attackTime < .65f)
            {
                attackTime += Time.deltaTime;

            }
            else
            {
                attackScript.StopAttack();
                attackTime = 0;
                
            }
        }

        if (isFollowingPlayer && playerParams.hp> 0)
        {
            SetMove(Player.transform.position);
        }
        else
        {
            isFollowingPlayer = false;
        }
        ExtraRotation();
    }
    public bool isBotAttack = false;
    public void AtackEvent()
    {
        
        //isDetectedPlayer = IsDetectedPlayer();
        var enemys = GetEnemyAttack1();
        //Debug.Log("Bot attack "+ isDetectedPlayer);
        if (enemys != null && enemys.Count > 0 && !GetComponent<BotControl>().isDead)
            //if (Vector3.Distance(transform.position, Player.transform.position) <= characterParams.attackDistance && isDetectedPlayer && !GetComponent<BotControl>().isDead )
        {
            foreach (GameObject go in enemys)
            {
                //if((UnityEngine.Random.Range(0, 10) > 4))
                {
                    if (go.tag == "Player")
                    {
                        go.GetComponent<PlayerControl>().TakenDamage(GetComponent<WeaponAttack>().rdWeapon);

                        //ListEnemy.Remove(go);

                        var temp = new Vector3(transform.localScale.x + 0.2f, transform.localScale.y + 0.2f, transform.localScale.z + .2f);
                        agent.speed = agent.speed * 1.2f;
                        transform.DOScale(temp, 1);
                        //FindObjectOfType<CameraController>().SetField();
                        characterParams.attackDistance += 1f;
                        //GameController.Instance.playerLeft.text = ListEnemy.Count.ToString();
                    }
                    else
                    {
                        go.GetComponent<BotControl>().TakenDamage(GetComponent<WeaponAttack>().rdWeapon);
                        playerParams.GetComponent<PlayerMovement>().ListEnemy.Remove(go);
                        GameController.Instance.playerLeft.text = playerParams.GetComponent<PlayerMovement>().ListEnemy.Count.ToString();
                        ListEnemy.Remove(go);
                        var temp = new Vector3(transform.localScale.x + 0.2f, transform.localScale.y + 0.2f, transform.localScale.z + .2f);
                        transform.DOScale(temp, 1);
                        //FindObjectOfType<CameraController>().SetField();
                        characterParams.attackDistance += 1f;
                    }

                }
               
            }

        }
        isFollowingPlayer = false;
        // isBotAttack = false;
    }
    private void RandomMove()
    {
        if (CharacterDetected("Bot"))
        {
            NavMesh.SamplePosition(agent.destination - transform.forward * wanderRadius, out navHit, wanderRadius, -1);
            agent.SetDestination(navHit.position);
        }
        else
        {
            if (agent.velocity.sqrMagnitude < MIN_VELOCITY)
            {
                MoveRandomNav(agent.destination, wanderRadius, -1);
            }
        }
        if (IsCanMove())
            agent.speed = botControl.isSlow ? botControl.slowSpeed : characterParams.moveSpeed;
    }
    List<GameObject> GetEnemyAttack()
    {
        
        List<GameObject> nearestAttackEnemy = new List<GameObject>();
       
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
        foreach (var enemy in ListEnemy)
        {
            if (!enemy.GetComponent<CharacterParams>().isDead)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                //if(nearestDistance == 0 || distance < nearestDistance)
                //{
                //    nearestDistance = distance;
                //    nearestEnemy = enemy;
                //}
                if (distance < characterParams.attackDistance )
                {
                    //Vector3 vec = transform.position;
                    //vec.y = transform.position.y + 0.1f;
                    //Ray ray = new Ray(vec, (enemy.transform.position - transform.position).normalized);
                    //Debug.DrawRay(vec, enemy.transform.position - transform.position, Color.red);
                    //RaycastHit hit;
                    //int layerMask = ~LayerMask.GetMask("Ignore Raycast");


                    Vector3 direction = new Vector3(enemy.transform.position.x - transform.position.x, transform.position.y, enemy.transform.position.z - transform.position.z);
                    float dis = Vector3.Distance(enemy.transform.position, transform.position);
                    Vector3 pro = Vector3.ProjectOnPlane(direction, transform.up);
                    Vector2 a = new Vector2(pro.x, pro.z);
                    Vector2 b = new Vector2(transform.forward.x, transform.forward.z);
                    float AngleDeg = Vector3.Angle(a, b);
                    //Debug.Log("Dir : "+ pro + "--- "+ AngleDeg);
                    if (((AngleDeg > 0 && AngleDeg < 30) || (AngleDeg > -30 && AngleDeg < 0)))
                    {  
                        if(UnityEngine.Random.Range(0, 100) > 90&&enemy.gameObject.tag=="Player")
                            nearestAttackEnemy.Add(enemy);
                        else  if (UnityEngine.Random.Range(0, 100) > 95 && enemy.gameObject.tag == "Bot")
                        {
                            nearestAttackEnemy.Add(enemy);
                        }
                    }
                }

            }
        }
       
      
        return nearestAttackEnemy;
    }
    float timeDelay = 0;
    List<GameObject> GetEnemyAttack1()
    {

        List<GameObject> nearestAttackEnemy = new List<GameObject>();

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
        foreach (var enemy in ListEnemy)
        {
            if (!enemy.GetComponent<CharacterParams>().isDead)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                //if(nearestDistance == 0 || distance < nearestDistance)
                //{
                //    nearestDistance = distance;
                //    nearestEnemy = enemy;
                //}
                if (distance < characterParams.attackDistance)
                {
                    //Vector3 vec = transform.position;
                    //vec.y = transform.position.y + 0.1f;
                    //Ray ray = new Ray(vec, (enemy.transform.position - transform.position).normalized);
                    //Debug.DrawRay(vec, enemy.transform.position - transform.position, Color.red);
                    //RaycastHit hit;
                    //int layerMask = ~LayerMask.GetMask("Ignore Raycast");


                    Vector3 direction = new Vector3(enemy.transform.position.x - transform.position.x, transform.position.y, enemy.transform.position.z - transform.position.z);
                    float dis = Vector3.Distance(enemy.transform.position, transform.position);
                    Vector3 pro = Vector3.ProjectOnPlane(direction, transform.up);
                    Vector2 a = new Vector2(pro.x, pro.z);
                    Vector2 b = new Vector2(transform.forward.x, transform.forward.z);
                    float AngleDeg = Vector3.Angle(a, b);
                    //Debug.Log("Dir : "+ pro + "--- "+ AngleDeg);
                    if (((AngleDeg > 0 && AngleDeg < 30) || (AngleDeg > -30 && AngleDeg < 0)))
                    {
                        if ( enemy.gameObject.tag == "Player")
                            nearestAttackEnemy.Add(enemy);
                        else if (enemy.gameObject.tag == "Bot")
                        {
                            nearestAttackEnemy.Add(enemy);
                        }
                    }
                }

            }
        }
        return nearestAttackEnemy;
    }
    private void DetectPlayer()
    {
        if(Vector3.Distance(transform.position,Player.transform.position) < detectedDistance)
        {
            if(UnityEngine.Random.Range(0,100)>97)
                isFollowingPlayer = true;
        }
        else
        {
            if(isFollowingPlayer&&!isFollowStart&&!characterParams.isDead)
                StartCoroutine(FollowPlayer());
            //Debug.Log(gameObject.name + " far: " + Vector3.Distance(transform.position, Player.transform.position));
        }
    }

    void ExtraRotation()
    {
        Vector3 lookrotation = agent.steeringTarget - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), rotationSpeed * Time.deltaTime);
    }

    private bool hasReachedDestination()
    {
        if (agent.enabled)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0.0f)
                {
                    return true;
                }
            }
            return false;
        }
        else return true;
    }

    private bool IsCanMove()
    {
        return !anim.GetBool("isStun");
    }
    private void MoveRandomNav(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
        randDirection += origin;
        
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        agent.updateRotation = true;
        anim.SetFloat("moveSpeed", 1);
        agent.SetDestination(navHit.position);

    }

    /// <summary>
    /// Update Nav, animation and destination of worker when worker moving
    /// </summary>
    /// <param name="target"></param>
    public void SetMove(Vector3 target)
    {
        agent.isStopped = false;
        agent.updateRotation = true;
        anim.SetFloat("moveSpeed", 1);
        agent.SetDestination(CalculateAgentTarget(target));
    }

    /// <summary>
    /// return the Gameobjects that detected.
    /// </summary>
    /// <returns></returns>
    bool CharacterDetected(string characterTag)
    {
        Vector3 direction = raycastTarget.position - gameObject.transform.position;
        RaycastHit hit;
        Ray ray = new Ray(gameObject.transform.position, direction);

        for (float i = 0; i <= DetectedWidth; i += 0.1f)
        {
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                if (hit.collider.gameObject != gameObject && hit.collider.CompareTag(characterTag))
                {
                    return true;
                }
            }

        }
        return false;
    }

    bool IsDetectedPlayer()
    {
       // Vector3 direction = new Vector3(Player.transform.position.x - transform.position.x, transform.position.y, Player.transform.position.z - transform.position.z);
        //float angle = Vector3.Angle(transform.forward, direction);
        Vector3 direction = new Vector3(Player.transform.position.x - transform.position.x, transform.position.y, Player.transform.position.z - transform.position.z);
        //float dis = Vector3.Distance(Player.transform.position, transform.position);
        //float AngleRad = Mathf.Atan2(direction.x - direction1.x, direction.z - direction1.z);
        // Get Angle in Degrees
        // float AngleDeg = AngleInDeg(direction, direction1);
        Vector3 pro = Vector3.ProjectOnPlane(direction, transform.up);

        Vector2 a = new Vector2(pro.x, pro.z);
        Vector2 b = new Vector2(transform.forward.x, transform.forward.z);
        float AngleDeg = Vector3.Angle(a, b);
        Vector3 vec = transform.position;
        vec.y = transform.position.y + 0.1f;
        //Debug.DrawRay(transform.position, direction,Color.red,10);
       
            if ( ((AngleDeg > 0 && AngleDeg < 30) || (AngleDeg > -30 && AngleDeg < 0))) return true;
       
        return false;
    }
    /// <summary>
    /// calculate the target of agent.
    /// </summary>
    Vector3 CalculateAgentTarget(Vector3 target)
    {
        var agentTarget = target;
        agentTarget.y = transform.position.y;
        NavMesh.CalculatePath(transform.position, agentTarget, NavMesh.AllAreas, path);
        return agentTarget;
    }
    bool isFollowStart = false;
    IEnumerator FollowPlayer()
    {
        isFollowStart = true;
        yield return new WaitForSeconds(timeToFollow);
        isFollowingPlayer = false;
        yield return new WaitForSeconds(UnityEngine.Random.Range(5,7));
        isFollowStart = false;
    }

    private void OnDisable()
    {
        if (EventDisPatcher.Instance != null)
            EventDisPatcher.Instance.RemoveListener(EventID.OnDetectPlayer, OnDetectPlayerRef);
    }
}
