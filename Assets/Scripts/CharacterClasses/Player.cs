using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
 
    public GameObject inventoryObj;
    public GameObject equipedWeapon;
    public float[] statistics;

    void InStats(float Str,float Vit, float Dex, float End, float Res, float Intel )
    {
        Vit = statistics[0];
        End = statistics[1];
        Str = statistics[2];
        Dex = statistics[3];
        Res = statistics[4];
        Intel = statistics[5];
    }

    void changeStat(int statID, float statVal)
    {
        statistics[statID] += statVal;
    }

    void FixedUpdate () {
	
	}
}
