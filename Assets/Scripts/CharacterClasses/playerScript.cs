using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {
 
    public GameObject inventoryObj;
    public GameObject equipedWeapon;
    public int[] statistics;

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
        // hi
    }

    void FixedUpdate () {
	
	}
}
