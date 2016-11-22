using UnityEngine;
using System.Collections;

public class AvoidWallTest : MonoBehaviour 
{
	RaycastHit hitInfo;

	Vector3 avoidForce;
	Vector3 rayDirection;
	Vector3 desiredVelocity;
	Vector3 oldLinear;

	float moveAhead;
	float maxEnemyDistance;
	float sphereCastRadius;

	int layerMask;

	Enemy me;
//	Wander wanderObj;

	public bool avoidWall;
	//public bool isUpdate;

	// Use this for initialization
	void Start () 
	{
		me = GetComponent<Enemy> ();
//		wanderObj = GetComponent<Wander>();
		avoidForce = Vector3.zero;
		oldLinear = Vector3.zero;
		moveAhead = 2f;
		layerMask = LayerMask.NameToLayer ("AI");
		maxEnemyDistance = 10f;
		sphereCastRadius = 1f;
		avoidWall = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		rayDirection = me.velocity + me.transform.position + (transform.forward * moveAhead);

		Ray ray = new Ray(me.transform.position,me.velocity + me.transform.position + (transform.forward * moveAhead));
		Debug.DrawLine(me.transform.position, rayDirection, Color.yellow);

		if (Physics.Raycast (transform.position, rayDirection, out hitInfo,layerMask)) 
//		{
//		if (Physics.SphereCast(ray, sphereCastRadius, out hitInfo, maxEnemyDistance, layerMask))
		{
			if (hitInfo.transform.gameObject.layer == layerMask) 
			{
				Debug.Log ("Enemy Hit the Wall");
				avoidWall = true;
				SetRotation ();

			}
		}
	}

	void SetRotation()
	{
		//Reflects a vector off the plane defined by a normal.
		avoidForce = Vector3.Reflect(me.velocity, hitInfo.normal);
//		Debug.Log ("Avoidance force : "+ avoidForce);
		//avoidForce = transform.right;
		avoidForce.Normalize ();
//		Debug.Log ("Avoid Force Normalized : " + avoidForce);

		if(avoidForce !=Vector3.zero)
		{
//			Debug.Log ("True");
			desiredVelocity= avoidForce * 5;
//			Debug.Log ("Desired Velocity : " + desiredVelocity);

			me.linear = desiredVelocity - me.velocity;
			me.velocity = desiredVelocity;
//			Debug.Log ("New linear value : " + me.linear);

			//me.transform.rotation = Quaternion.LookRotation(new Vector3 (0, Random.Range (-90f, 90f), 0));

			me.angular = new Vector3 (0, 0,Random.Range (-90f, 90f));
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere (transform.position, sphereCastRadius);
	}
}


/*// Calculate desired velocity
m_DesiredVelocity = (avoidanceForce).normalized * SteeringCore.MaxSpeed;

// Calculate steering force
SteeringForce = m_DesiredVelocity - SteeringCore.Velocity;
m_OldValidSteeringForce = SteeringForce;
m_SteeringForceConservationTimer = 0;
*/