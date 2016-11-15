using UnityEngine;
using System.Collections.Generic;

public abstract class Formation : MonoBehaviour {

    public float distanceMultToEach;
    public bool is2D;

    public abstract List<Vector3> FormationPosition();
}
