using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTrap : TrapBase
{
    public override TrapKind trapKind { get { return TrapKind.Rocket; } }

    [SerializeField] private GameObject Rocket;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionPosY;
    [SerializeField] private float timeActive, timeDrop;

    private CharacterParams[] characterParams;
    private Vector3 ExplosionPos, oriPos;

    // Start is called before the first frame update
    void Start()
    {
        characterParams = FindObjectsOfType<CharacterParams>();
        oriPos = Rocket.transform.position;
        ExplosionPos = new Vector3(transform.position.x, explosionPosY, transform.position.z);
        DropRocket();
    }

    private void DropRocket()
    {
        float random = Random.Range(timeActive - 2, timeActive + 2);
        TweenControl.GetInstance().DelayCall(transform, random, () =>
        {
            Rocket.transform.position = oriPos;
            Rocket.SetActive(true);
            TweenControl.GetInstance().Move(Rocket.transform, ExplosionPos, timeDrop, () =>
            {
                Rocket.SetActive(false);
                explosionEffect.Play();
                ExplosionCharacter();
                DropRocket();
            });
        });
    }

    private void ExplosionCharacter()
    {
        Debug.Log(Vector3.Distance(ExplosionPos, GameObject.FindGameObjectWithTag("Player").transform.position));
        foreach(var character in characterParams)
        {
            if(Vector3.Distance(ExplosionPos, character.transform.position) <= explosionRadius)
            {
                var iTakeDmg = character.gameObject.GetComponent<IStepOnTrap>();
                iTakeDmg.StepOnBless(trapData);
            }
        }
    }
}
