using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBallTrap : TrapBase
{
    public override TrapKind trapKind { get { return TrapKind.SkullBall; } }
    [SerializeField] private GameObject SkullBall;
    [SerializeField] private GameObject effect;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float timeSpawn;

    public Vector3 beginPos, targetPos;
    //private ParticleSystem Effect;
    private Vector3 direction;
    private Vector3 currTarget, currRot;

    private void Start()
    {
        //Effect = GetComponentInChildren<ParticleSystem>();
        transform.position = beginPos;
        currTarget = targetPos;
        direction = (targetPos - beginPos).normalized;
        currRot = Vector3.forward;
    }

    private new void Update()
    {
        base.Update();

        transform.Translate(direction * Time.deltaTime * moveSpeed, Space.World);
        SkullBall.transform.Rotate(currRot * Time.deltaTime * rotSpeed);

        if (Vector3.Distance(transform.position, currTarget) < 0.1f)
        {
            if(currTarget == targetPos)
            {
                currTarget = beginPos;
                direction = (currTarget - targetPos).normalized;
                currRot = Vector3.back;
            }
            else
            {
                currTarget = targetPos;
                direction = (targetPos - beginPos).normalized;
                currRot = Vector3.forward;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (InitPlay) if (!InitPlay.isStart) return;
        var dmgTrap = other.GetComponent<IStepOnTrap>();
        if (dmgTrap != null)
        {
            dmgTrap.StepOnBless(trapData);
            //Effect.Play();
        }
    }

}
