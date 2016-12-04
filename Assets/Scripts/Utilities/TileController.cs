using UnityEngine;
using System.Collections;

public class TileController : MonoBehaviour {

    public GameObject currentTile;

    // Find the nearest tile
    public void Tile()
    {
        float shortestDistance = Mathf.Infinity;
        foreach (GameObject obj in FindObjectOfType<LevelGenerator>().aTilePos)
        {
            float dist = (obj.transform.position - transform.position).magnitude;
            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                currentTile = obj;
            }
        }
    }

    void Update()
    {
        if(currentTile == null)
            Tile();
    }
}
