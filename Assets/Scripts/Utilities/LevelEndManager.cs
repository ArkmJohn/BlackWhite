using UnityEngine;
using System.Collections;

public class LevelEndManager : MonoBehaviour {

    public delegate void KillEnemies();
    public delegate void WinGame();

    public static KillEnemies killEnemies;

}
