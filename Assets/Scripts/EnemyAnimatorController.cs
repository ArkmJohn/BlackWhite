using UnityEngine;
using System.Collections;

public class EnemyAnimatorController : MonoBehaviour {

    public Animator anim;
    Enemy me;

	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
        me = GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (me.velocity != Vector3.zero)
        {
            anim.SetFloat("Walking", 1);
        }
	}

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }
}
