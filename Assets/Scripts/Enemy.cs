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

			/*float angle = Vector3.Angle (rotation, transform.position);
			//Debug.Log ("Angle : "+angle);
			transform.eulerAngles = new Vector3(0 , angle, 0);
			angular = new Vector3 (0, angle, 0);
*/
			transform.rotation = Quaternion.LookRotation (rotation);
        }
        else if(isDummy)
        {
            float Dist = Vector3.Distance(transform.position, FindObjectOfType<CharacterControl>().gameObject.transform.position);

            //Debug.Log("Enemy is " + Dist + " to the player");
            if (Dist <= 15)
            {
                Debug.Log("Chasing Player");
                Vector3 target = new Vector3(FindObjectOfType<CharacterControl>().gameObject.transform.position.x, transform.position.y, FindObjectOfType<CharacterControl>().gameObject.transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            }
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
    public void AttackAnim()
    { }
    public void WalkAnim()
    { }
    public void StandAnim()
    { }
    public void DieAnim()
    { }
}
