using UnityEngine;
using System.Collections;
using System;

public class ConsumableItem : Item {

    [SerializeField]
    private int statID;
    [SerializeField]
    public float statVal;
    
    public override void Pickup()
    {
        
    }

    public override void UseItem()
    {
        changeStat(myChar);
    }

    // Call the function on the character using some stats
    private void changeStat(Player target)
    {
    }
}
