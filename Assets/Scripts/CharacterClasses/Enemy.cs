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
    public float angular;
    public float maxAccel;
    public bool isDummy;
    public GameObject myNode;
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
            transform.rotation = Quaternion.LookRotation(rotation);
            //myNode = NearestNode();
        }
        else
        {
            float Dist = Vector3.Distance(transform.position, FindObjectOfType<CharacterControl>().gameObject.transform.position);
            if (Dist < 5)
            {
                transform.position = Vector3.MoveTowards(transform.position, FindObjectOfType<CharacterControl>().gameObject.transform.position, _speed * Time.deltaTime);
            }
        }
    }

    public void LateUpdate()
    {
        if (!isDummy)
        {
            velocity += linear * Time.deltaTime;

            // Calculates the speed if it is faster or slower than intended
            if (velocity.magnitude > _speed)
            {
                velocity.Normalize();
                velocity *= _speed;
            }

            if (linear.sqrMagnitude == 0)
                velocity = Vector3.zero;
        }
    }

    public GameObject NearestNode()
    {
        Debug.Log("WTF");

        GameObject cNode = null;
        float shortestDistance = Mathf.Infinity;
        TileAI[] tileAI = FindObjectsOfType<TileAI>();
        foreach (TileAI obj in tileAI)
        {
            float dist = (obj.gameObject.transform.position - transform.position).magnitude;
            Debug.Log("Hello!");
            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                cNode = obj.gameObject;
            }
        }
        return cNode;
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
