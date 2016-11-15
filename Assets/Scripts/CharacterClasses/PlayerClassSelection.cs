using UnityEngine;
using System.Collections.Generic;

public class PlayerClassSelection : MonoBehaviour {

    private string[] PlayerClass;
    public List<GameObject> equipables;
    public Player myPlayer;
    GameManager gm;

    void Start()
    {
        myPlayer = FindObjectOfType<Player>();
        gm = FindObjectOfType<GameManager>();
    }
	public void Tank()
    {
        myPlayer.InStats(12,8,7,8,10,5);
        gm.startWeaponPrefab = equipables[0];
        Debug.Log(myPlayer.statistics);
    }
    public void DPSMele()
    {
        myPlayer.InStats(10,6,15,6,8,5);
        gm.startWeaponPrefab = equipables[1];
        Debug.Log("DPSmele");

    }
   public void DPSRange()
    {
        myPlayer.InStats(8,8,12,10,7,5);
        gm.startWeaponPrefab = equipables[2];
        Debug.Log("DPSrange");

    }
    public void BalancedRanged()
    {
        myPlayer.InStats(10,8,10,8,9,5);
        gm.startWeaponPrefab = equipables[3];
        Debug.Log("balancedR");

    }
    public void Balanced()
    {
        myPlayer.InStats(5,5,5,5,0,15);
        gm.startWeaponPrefab = equipables[4];
        Debug.Log("balanced");

    }

    public void EXBu(GameObject op)
    {
        Debug.Log("balan");
    }

}
