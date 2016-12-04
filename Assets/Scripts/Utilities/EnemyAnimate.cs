using UnityEngine;
using System.Collections;

public class EnemyAnimate : MonoBehaviour {

    public void StopAttack()
    {
        GetComponentInParent<Enemy>().StopAttack();
    }

    public void ActivateColl()
    {
        GetComponentInParent<Enemy>().ActivateAttackCollider();
    }
}
