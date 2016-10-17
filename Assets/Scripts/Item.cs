using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour {

    public int id;

    public virtual void Pickup()
    {
        // Add this item to the inventory
    }

    public abstract void UseItem();
}
