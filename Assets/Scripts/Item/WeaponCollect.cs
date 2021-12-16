using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WeaponCollect : MonoBehaviour
{
    [SerializeField] private float multiplyScale = 1.5f;
    private Vector3 oriPos, oriScale;
    // Start is called before the first frame update
    void Awake()
    {
        oriPos = transform.position;
        oriScale = transform.localScale;
        transform.localScale *= multiplyScale;
        DG.Tweening.DOVirtual.DelayedCall(0.2f, () => { Destroy(GetComponentInParent<NavMeshAgent>()); });
    }

    private void OnTriggerEnter(Collider other)
    {
        //var wpCollector = other.GetComponent<ITakeWeapon>();
        //if (wpCollector != null)
        //{
        //    if (wpCollector.HaveWeapon()) return;
        //    if (other.CompareTag("Player")) FindObjectOfType<ActiveWeaponButton>().isPlayerCollect = true;
            
        //    Destroy(transform.parent.GetComponentInChildren<ParticleSystem>().gameObject);
        //    wpCollector.TakeWeapon(gameObject, oriPos, transform.rotation, oriScale);
        //    Destroy(this);
        //}
    }
}
