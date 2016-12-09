using UnityEngine;
using System.Collections;

public class Enemy : Character
{
    public Vector3 targetLocation;
    public Tree myAITree;
    public GameObject target;
    public Vector3 velocity;
    public Vector3 position;
    public Node routine;

    public Vector3 linear;
    public Vector3 rotation;
	public Vector3 angular;
    public float maxAccel;
    public bool isDummy;
	public bool isAvoidWall;
    public bool isAttacking = false;
    public GameObject attackCollider;

	public Vector3 tempRotation;
    public Group myGroup;

    public void Init()
    {
		myAITree = GetComponent<TestTree> ();
        routine.Start();
        LevelEndManager.killEnemies += Death;
    }

    // This is where the enemy should put all its ai
    protected void Act()
    {
        myAITree.AIAct();
        // Do some other stuff

        if (Health > 0)
        {
            routine.act(this);
        }
    }

    public void Tick()
    {
        if (Health > 0)
        {
            routine.act(this);
        }
    }
    public override void Death()
    {
        LevelEndManager.killEnemies -= Death;
        base.Death();
    }
    void Update()
	{

		if(target == null)
		{
		//	target = GameObject.Find ("Johnny Bravo 3.0(Clone)");
			target = GameObject.FindGameObjectWithTag ("Player");
			Debug.Log ("The target is : " + target);
		}

        if (!isDummy && !isAttacking)
        {
            Vector3 displacement = velocity * Time.deltaTime;

            Vector3 pos = new Vector3(displacement.x, 0, displacement.z);

            transform.Translate(new Vector3(pos.x, 0, pos.z), Space.World);

            /*float angle = Vector3.Angle (rotation, transform.position);
			//Debug.Log ("Angle : "+angle);
			transform.eulerAngles = new Vector3(0 , angle, 0);
			angular = new Vector3 (0, angle, 0);
*/
            //transform.rotation = Quaternion.LookRotation (rotation);
			transform.LookAt (transform.position + velocity);
			//transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
            //transform.rotation = Quaternion.Euler(rotation);
        }
    }
    public override void GetDamaged(Character attacker)
    {
        base.GetDamaged(attacker);
    }
    public override void CheckIfDead()
    {
        base.CheckIfDead();
    }
    public void LateUpdate()
    {
        if (!isDummy)
        {
			//linear += wallAvoidVIndent;
            velocity += linear * Time.deltaTime;

            // Calculates the speed if it is faster or slower than intended
            if (velocity.magnitude > _speed)
            {
                velocity.Normalize();
                velocity *= _speed;
            }
			//angular += wallAvoidIndent;
			rotation += angular;

            if (linear.sqrMagnitude == 0)
                velocity = Vector3.zero;
        }
    }

	/*void OnDrawGizmos()
	{
		// checking with velocity
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, transform.position + velocity);

		// With rotation
		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, transform.position + rotation);
	}*/

    // TODO: Set animations and stuff
    public void AttackInFront()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            GetComponent<EnemyAnimatorController>().Attack();
            //attackCollider.SetActive(true);
        }
    }

    public void ActivateAttackCollider()
    {
        attackCollider.SetActive(true);
    }
    public void StopAttack()
    {
        Debug.Log("Ello");
        attackCollider.SetActive(false);
        isAttacking = false;
    }
}
