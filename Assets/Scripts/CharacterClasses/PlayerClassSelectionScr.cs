using UnityEngine;
using System.Collections.Generic;

public class PlayerClassSelectionScr : MonoBehaviour {

    private string[] PlayerClass;
    private Player myPlayer;


    private void Start()
    {
        myPlayer = FindObjectOfType<Player>();
    }
	public void Tank()
    {
        myPlayer.InStats(12,8,7,8,10,5);
        Debug.Log(myPlayer.statistics);
    }
    public void DPSMele()
    {
        myPlayer.InStats(10,6,15,6,8,5);
        Debug.Log("DPSmele");

    }
   public void DPSRange()
    {
        myPlayer.InStats(8,8,12,10,7,5);
        Debug.Log("DPSrange");

    }
    public void BalancedRanged()
    {
        myPlayer.InStats(10,8,10,8,9,5);
        Debug.Log("balancedR");

    }
    public void Balanced()
    {
        myPlayer.InStats(5,5,5,5,0,15);
        Debug.Log("balanced");

    }

    public void EXBu(GameObject op)
    {
        Debug.Log("balan");
    }

}
