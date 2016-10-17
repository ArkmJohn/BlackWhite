using UnityEngine;
using System.Collections;

public class DamageScript : MonoBehaviour {
    
    public int damageType;
    private enum BDType
    {
        NULL,
        SLICE,
        IMPACT,
        PIERCE

    }
    private enum SDType
    {
        NULL,
        ELECTRIC,
        ICE,
        FIRE
    }




    public void Type() { }
}
