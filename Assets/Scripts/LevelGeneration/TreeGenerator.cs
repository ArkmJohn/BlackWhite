using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeGenerator : MonoBehaviour {

    public List<GameObject> treePrefabs;

    public GameObject GetRandomTree()
    {
        return treePrefabs[Random.Range(0,treePrefabs.Count)];
    }
}

