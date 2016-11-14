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

	public Vector3 tempRotation;

    public void Init()
    {
        routine.Start();
    }

    // This is where the enemy should put all its ai
    protected void Act()
    {
        //myAITree.AIAct();
        // Do some other stuff

        if (Health > 0)
        {
            routine.act(this);
        }
    }

    public void Tick()
    {
        Act();
    }

    void Update()
	{

        if (!isDummy)
        {
            Vector3 displacement = velocity * Time.deltaTime;

            transform.Translate(displacement, Space.World);
			float angle = Vector3.Angle (rotation, transform.position);
			Debug.Log (angle);
			transform.eulerAngles = new Vector3(0 , angle, 0);


        }

		if(isAvoidWall)
		{
			//linear = Vector3.zero;
			//velocity = Vector3.zero;
			//rotation = Vector3.zero;
			//rotation = tempRotation;

		}

		if(!isAvoidWall)
		{
		//	linear = new Vector3(0,0,0);
		//	velocity = new Vector3(1,0,1);

			//rotation = tempRotation;
			//transform.position += transform.forward * (2 * Time.deltaTime);
		}

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
			rotation = angular;

            if (linear.sqrMagnitude == 0)
                velocity = Vector3.zero;
        }

    }

	void OnDrawGizmos()
	{
		Gizmos.DrawSphere (velocity + transform.position, 1);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere (rotation + transform.position, 1);
	}

    // TODO: Set animations and stuff
    public void AttackAnim()
    { }
    public void WalkAnim()
    { }
    public void StandAnim()
    { }
    public void DieAnim()
    { }
}
