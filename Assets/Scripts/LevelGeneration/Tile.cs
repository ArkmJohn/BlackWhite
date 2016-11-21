using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Tile : MonoBehaviour {

    public enum tileState
    {
        VOID,
        WALL,
        EMPTY
    }

    public tileState myState = tileState.EMPTY;
    public int neighbourTileCount;
    public bool isOuterWall, Trees, isTesting = false;

    public List<GameObject> neighbours = new List<GameObject>();
    public float tileRad = 5;
    public LayerMask nodeLayerMask;
    public LayerMask collisionLayerMask;
    public int aCost, bCost;

    public void  setTile(int neighbourCount, bool iOuterWall, GameObject wallParent, GameObject emptyTParent, GameObject voidParent, bool isTreeLevel)
    {
        
        nodeLayerMask = LayerMask.GetMask("Tile");
        collisionLayerMask = LayerMask.GetMask("Tile", "Collision");
        Trees = isTreeLevel;
        if (neighbourCount == 0)
        {
            gameObject.name = "TileAI";
            gameObject.layer = 9;
            myState = tileState.EMPTY;
            gameObject.transform.SetParent(emptyTParent.transform);
            gameObject.AddComponent<SphereCollider>();
            gameObject.AddComponent<TileAI>();
            GetComponent<Collider>().isTrigger = true;
            gameObject.tag = "TileAI";
        }
        else if (neighbourCount == 8)
        {
            myState = tileState.VOID;
            gameObject.transform.SetParent(voidParent.transform);

            VoidTileSetUp();
        }
        else
        {
            myState = tileState.WALL;
            gameObject.transform.SetParent(wallParent.transform);

            WallTileSetup();
        }

        neighbourTileCount = neighbourCount;
        isOuterWall = iOuterWall;
    }

    void VoidTileSetUp()
    {
        gameObject.AddComponent<BoxCollider>();
        BoxCollider col = GetComponent<BoxCollider>();
        col.isTrigger = true;
        if (Trees)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            TreeGenerator tG = FindObjectOfType<TreeGenerator>();
            GameObject tree = (GameObject)Instantiate(tG.GetRandomTree(), pos, transform.rotation);
            tree.transform.SetParent(this.transform);
            tree.name = "Tree";
        }
    }

    void WallTileSetup()
    {
        gameObject.AddComponent<BoxCollider>();
        if (Trees)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            TreeGenerator tG = FindObjectOfType<TreeGenerator>();
            GameObject tree = (GameObject)Instantiate(tG.GetRandomTree(), pos, transform.rotation);
            tree.transform.SetParent(this.transform);
            tree.name = "Tree";
        }
        gameObject.layer = 8;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<CharacterControl>() != null && neighbourTileCount == 8)
        {
            col.gameObject.GetComponent<CharacterControl>().VoidHit();
        }
    }

    void OnDrawGizmos()
    {
        if (neighbourTileCount == 0)
        {
            if (isTesting)
            {
                Gizmos.DrawSphere(transform.position, 1);
                foreach (GameObject neighbor in neighbours)
                {
                    Gizmos.DrawLine(transform.position, neighbor.transform.position);
                    Gizmos.DrawWireSphere(neighbor.transform.position, 0.25f);
                }
            }
        }
    }

    [ContextMenu("Find Neighbouring Tiles")]
    public void FindNeighbours()
    {
        neighbours.Clear();

        Collider[] cols = Physics.OverlapSphere(transform.position, tileRad, nodeLayerMask);
        foreach (Collider collidedTile in cols)
        {
            if (collidedTile.gameObject != gameObject)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, (collidedTile.transform.position - transform.position), out hit, tileRad, collisionLayerMask);

                if (hit.transform != null)
                {
                    if (hit.transform.gameObject == collidedTile.gameObject)
                    {
                        neighbours.Add(collidedTile.gameObject);
                    }

                }
            }
        }
    }

    public int cCost
    {
        get
            {
            return aCost + bCost;
            }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
