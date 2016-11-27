using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    public Sprite deleteS;
    public Sprite deleteSelectedS;
    public bool isDeleting = false;

    public GameObject slotPrefab;
    public float size; //size of the slot
    private List<GameObject> allSlots; //this list will contain all the slots in our inventory
    private static int blankSlots; //keep track of how many empty slots are in inventory

    //to create the inventory slots
    private RectTransform invRect;
    public int slots;
    public int rows;
    public float leftSpace, topSpace; //padding needed on the sides of the inventory
    private float invWidth; //width of the inventory
    private float invHeight; //height of the inventory

    //using property to access blankSlots in other scripts
    public static int BlankSlots
    {
        get { return blankSlots; }

        set { blankSlots = value; }
    }

    void Start()
    {
        CreateSlots();
    }


    private void CreateSlots()
    {
        blankSlots = slots - 1;

        //instantiate the list of slots
        allSlots = new List<GameObject>();

        //calculate the width of the inventory
        invWidth = (slots / rows) * (size + leftSpace) + leftSpace;

        //calculate the height of the inventory
        invHeight = rows * (size + topSpace) + topSpace;

        invRect = GetComponent<RectTransform>();

        //resizing the inventory according to the calculated width and height
        invRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, invWidth);
        invRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, invHeight);

        int columns = slots / rows;

        //runs through the rows (y-axis)
        for (int i = 0; i < rows; i++)
        {
            //runs through the columns (x-axis)
            for (int j = 0; j < columns; j++)
            {
                //create a new slot
                GameObject newSlot = (GameObject)Instantiate(slotPrefab);

                //create the transform to move the object around
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                //change name of the GameObject instantiated
                newSlot.name = "Slot";

                //when slot instantiates, we will set the parent as the canvas 
                newSlot.transform.SetParent(this.transform.parent);

                //set the xy position of each slot
                slotRect.localPosition = invRect.localPosition + new Vector3(leftSpace * (j + 1) + (size * j), -topSpace * (i + 1) - (size * i));

                //set the size of the slots
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);

                newSlot.transform.SetParent(this.gameObject.transform);

                if (j + 1 == slots)
                {
                    newSlot.GetComponent<Slot>().UpdateSprite(deleteS, deleteSelectedS);
                    newSlot.GetComponent<Slot>().deleteSlot = true;

                }
                else
                    allSlots.Add(newSlot);
            }
        }
    }


    public bool AddItem(GameObject itemObject)
    {
        Item itemScript = itemObject.GetComponent<Item>();

        //if the item we want to add has max stack size of 1
        if (itemScript.maxSize == 1)
        {
            //place in blank slot since it's not stackable
            fillBlankSlot(itemObject);
            return true;
        }

        else
        {
            foreach (GameObject slot in allSlots)
            {
                //need to check what slot we can stack item on
                Slot tempSlot = slot.GetComponent<Slot>();

                //if we find a slot that is occupied, we will check if it can be stacked
                if (!tempSlot.isEmpty)
                {
                    //if the item we collected is same as item in that slot
                    if (tempSlot.currentItem.id == itemScript.id && tempSlot.isAvailable)
                    {
                        //add item collected to the slot stack
                        tempSlot.AddItem(itemObject);
                        return true;
                    }
                }

                if (blankSlots > 0)
                {
                    fillBlankSlot(itemObject);
                }
            }
        }
        return false;
    }

    //run through every single slot in the collection to check for blank slot
    private bool fillBlankSlot(GameObject itemObject)
    {
        Item item = itemObject.GetComponent<Item>();
        if (blankSlots > 0)
        {
            foreach (GameObject slot in allSlots)
            {
                Slot tempSlot = slot.GetComponent<Slot>();

                if (tempSlot.isEmpty)
                {
                    tempSlot.AddItem(itemObject); //place item in slot 
                    blankSlots--; //remove a blank slot
                    return true;
                }
            }
        }
        return false;
    }
}
