using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GroupMemberAI : Enemy {

    public bool isGroupLeader = false;
    public bool isUsingNodes = false;
    GameObject movePos;
    Stack<GameObject> path;
    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isDummy && !isAttacking)
        {

            if (isGroupLeader)
            {
                Wander();
            }
            else
            {

            }

        }
	
	}

    void Wander()
    {
        if (movePos == null)
        {
            movePos = newTarget();
        }
        else if (isNear(movePos.transform.position))
            movePos = newTarget();

        GoToPosition(movePos);
    }



    public void GoToPosition(GameObject target)
    {
        //Debug.Log(gameObject.name + " Moving to " + nextPos);

        // Check if we are in the node and if not move towards the node
        if (isUsingNodes)
        {
            /*if (isNearNode(target.transform.position))
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, _speed * Time.deltaTime);
                Rotate(target.transform.position);

            }
            else
            {
                Vector3 a = Vector3.zero;
                if (a == Vector3.zero)
                    a = NextPos(target);
                else if (isNear(a)) 
                    a = NextPos(target);

                transform.position = Vector3.MoveTowards(transform.position, a, _speed * Time.deltaTime);
                Rotate(a);

            }*/
            //Debug.Log(FindObjectOfType<LevelGenerator>().aTilePos.Count + GetComponent<TileController>().currentTile.name);
            if (GetComponent<TileController>().currentTile != null)
            {

                if (path == null)
                {
                    path = FindPath(target);
                }

                Debug.Log(path.Count + gameObject.name);
                GameObject pos = path.Pop();
                transform.position = Vector3.MoveTowards(transform.position, pos.transform.position, _speed * Time.deltaTime);

                if(isNear(pos.transform.position))
                {
                    gameObject.GetComponent<TileController>().Tile();
                    path = FindPath(target);
                }
            }

            //Stack<GameObject> path = Dikstras.dijkstra(FindObjectOfType<LevelGenerator>().aTilePos.ToArray(), GetComponent<TileController>().currentTile, target);
            //GameObject pos = path.Pop();
            //transform.position = Vector3.MoveTowards(transform.position, pos.transform.position, _speed * Time.deltaTime);

            /*if (isNear(pos.transform.position))
            {
                gameObject.GetComponent<TileController>().Tile();
            }*/
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, _speed * Time.deltaTime);

            Rotate(target.transform.position);
        }
    }

    Stack<GameObject> FindPath(GameObject target)
    {
        return Dikstras.dijkstra(FindObjectOfType<LevelGenerator>().aTilePos.ToArray(), GetComponent<TileController>().currentTile, target);
    }

    void Rotate(Vector3 nextPos)
    {
        Vector3 tDir = nextPos - transform.position;
        float step = _speed * Time.deltaTime;
        Vector3 nDir = Vector3.RotateTowards(transform.forward, tDir, step, 0.0F);
        Debug.DrawRay(transform.position, nDir, Color.red);
        transform.rotation = Quaternion.LookRotation(nDir);
    }

    GameObject newTarget()
    {
        GameObject pos;
        pos = FindObjectOfType<LevelGenerator>().aTilePos[Random.Range(0, FindObjectOfType<LevelGenerator>().aTilePos.Count)];
        return pos;
    }

    bool isNear(Vector3 mySupposedPos)
    {
        if (Vector3.Distance(transform.position, mySupposedPos) < 2)
        {
            return true;
        }
        else
            return false;
    }
    bool isNearNode(Vector3 mySupposedPos)
    {
        if (Vector3.Distance(transform.position, mySupposedPos) < 5)
        {
            return true;
        }
        else
            return false;
    }
}
