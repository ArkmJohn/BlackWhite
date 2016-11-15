using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerClassSelection : MonoBehaviour {

    private string[] PlayerClass;
    public Text Vit, End, Str, Dex, Res, Int;
    public List<GameObject> equipables;
    public Player myPlayer;
    GameManager gm;

    void Start()
    {
        myPlayer = FindObjectOfType<Player>();
        gm = FindObjectOfType<GameManager>();
        Balanced();
    }
	public void Tank()
    {
        myPlayer.InStats(12,8,7,8,10,5);
        gm.startWeaponPrefab = equipables[0];
        Debug.Log(myPlayer.statistics);
        LoadMyText(12, 8, 7, 8, 19, 5);
    }
    public void DPSMele()
    {
        myPlayer.InStats(10,6,15,6,8,5);
        gm.startWeaponPrefab = equipables[1];
        Debug.Log("DPSmele");
        LoadMyText(10, 6, 15, 6, 8, 5);
    }
   public void DPSRange()
    {
        myPlayer.InStats(8,8,12,10,7,5);
        gm.startWeaponPrefab = equipables[2];
        Debug.Log("DPSrange");
        LoadMyText(8, 8, 12, 10, 7, 5);

    }
    public void BalancedRanged()
    {
        myPlayer.InStats(10,8,10,8,9,5);
        gm.startWeaponPrefab = equipables[3];
        Debug.Log("balancedR");
        LoadMyText(10, 8, 10, 8, 9, 5);

    }
    public void Balanced()
    {
        myPlayer.InStats(5,5,5,5,0,15);
        gm.startWeaponPrefab = equipables[4];
        Debug.Log("balanced");
        LoadMyText(5, 5, 5, 5, 0, 15);
    }

    void LoadMyText(float v, float e, float s, float d, float r, float i)
    {
        Vit.text = v.ToString();
        End.text = e.ToString();
        Str.text = s.ToString();
        Dex.text = d.ToString();
        Res.text = r.ToString();
        Int.text = i.ToString();
    }

    public void EXBu(GameObject op)
    {
        Debug.Log("balan");
    }

    public void LoadGame()
    {
        ButtonManager bm = FindObjectOfType<ButtonManager>();
        bm.LoadLevel("LevelScene");
    }
}
