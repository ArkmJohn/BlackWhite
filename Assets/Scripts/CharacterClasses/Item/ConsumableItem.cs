using UnityEngine;
using System.Collections;
using System;

public class ConsumableItem : Item {

    [SerializeField]
    private int statID;
    [SerializeField]
    private float statVal;

    public override void UseItem()
    {
        Player p = FindObjectOfType<Player>();

        if (p != null)
        {
            p.changeStat(statID, statVal);
        }
    }
}
