using UnityEngine;
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
    public bool isOuterWall;

    public void  setTile(int neighbourCount, bool iOuterWall, GameObject wallParent, GameObject emptyTParent, GameObject voidParent)
    {
        if (neighbourCount == 0)
        {
            myState = tileState.EMPTY;
            gameObject.transform.SetParent(emptyTParent.transform);
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
    }

    void WallTileSetup()
    {
        gameObject.AddComponent<BoxCollider>();
        
        gameObject.layer = 8;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<CharacterControl>() != null)
        {
            col.gameObject.GetComponent<CharacterControl>().VoidHit();
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
