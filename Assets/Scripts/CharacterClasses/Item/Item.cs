using UnityEngine;
using System.Collections;

//public enum ItemType { EQUIP, CONSUMABLE, WEAPON };

public class Item : MonoBehaviour
{
    public int id;
    public Player myChar;
    public bool isUsed = false;
    //public ItemType type;
    public int maxSize; //max amount to stack an item
    public string itemName, flavorText;
    public Sprite sprNeutral, sprHighlighted;



    public virtual void UseItem()
    {
        //switch (type)
        //{
        //    case ItemType.CONSUMABLE:
        //        Debug.Log("used a consumable");
        //        break;

        //    case ItemType.EQUIP:
        //        Debug.Log("used an equipment");
        //        break;          
        //}
        
    }

    //public abstract void Pickup();

}