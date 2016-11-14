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

        FindObjectOfType<FloatUI>().UseIText(flavorText);
        FindObjectOfType<FloatUI>().UseStatText("+" + statVal.ToString() + " " + GetStatName());
    }

    string GetStatName()
    {
        switch (statID)
        {
            case 0:
                return "Vit";
            case 1:
                return "End";
            case 2:
                return "Str";
            case 3:
                return "Dex";
            case 4:
                return "Res";
            case 5:
                return "Int";
            default:
                return "Null";
        }
    }
}
