using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    GameObject currentItemA;
    public int itemStackCount;
    private Stack<Item> itemStack;
    public Text stackText; //text to display the number of the item collected 
    public bool deleteSlot = false;
    public Sprite emptyS;
    public Sprite highlightS;


    // Use this for initialization
    void Start()
    {
        //instantiating our items
        itemStack = new Stack<Item>();

        SetSlot();
    }

    private void SetSlot()
    {
        //set up our rect
        RectTransform slotRect = GetComponent<RectTransform>();
        RectTransform textRect = stackText.GetComponent<RectTransform>();

        //making the text scale to 60% of the slot
        //since it returns a double, we will cast it into an int
        int textScale = (int)(slotRect.sizeDelta.x * 0.60);

        //set the max and min size of the text
        stackText.resizeTextMinSize = textScale;
        stackText.resizeTextMaxSize = textScale;

        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
    }


    public void UpdateSprite(Sprite neutral, Sprite highlight)
    {
        //set the standard sprite
        GetComponent<Image>().sprite = neutral;

        //reference to the SpriteState component of the Image script
        SpriteState spriteState = new SpriteState();

        //set highlighted and pressed sprite
        spriteState.highlightedSprite = highlight;
        spriteState.pressedSprite = neutral;

        //change the button sprite
        GetComponent<Button>().spriteState = spriteState;
    }


    public void AddItem(GameObject itemObject)
    {
        Item item = itemObject.GetComponent<Item>();
        //adding items to the stack
        itemStack.Push(item);
        //Debug.Log("ITEM IS ADDED IN STACk");


        //write text on the screen according to the items stacked in the slot
        if (itemStack.Count > 1)
        {
            //if there's > 1 item in slot, display text
            stackText.text = itemStack.Count.ToString();
        }

        //update sprite when item is stacked
        UpdateSprite(item.sprNeutral, item.sprHighlighted);
        //Debug.Log("SPRITE UPDATED");
        itemStackCount = itemStack.Count;
        currentItemA = itemObject;
    }


    private void UseItem()
    {
        //if item is in the slot
        if (!isEmpty)
        {
            //we can use the top item in the stack
            itemStack.Pop().UseItem();

            //if item number of items in one slot is greater than 1
            if (itemStack.Count > 1)
            {
                //display number of items in the slot
                stackText.text = itemStack.Count.ToString();
            }

            else
            {
                // do not display number of items since it is 1
                stackText.text = string.Empty;
            }


            if (isEmpty)
            {
                //if we used the last item in inventory, we'll replace it with the standard slot Sprite
                UpdateSprite(emptyS, highlightS);

                //increase number of blankSlots
                Inventory.BlankSlots++;
            }
        }
    }


    //using the interface IPointerClickHandler 
    // PointerEventData has data of the item we clicked on
    public void OnPointerClick(PointerEventData eventData)
    {
        //if we right-click pointer button 
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (deleteSlot)
                isDeleting();
            else if (gameObject.GetComponentInParent<Inventory>().isDeleting)
                RemoveItem();
            else
                UseItem();
        }
    }

    //to check if item can stack on itself
    public Item currentItem
    {
        //look at the first item in the item stack
        get { return itemStack.Peek(); }
    }

    //to check if slot is empty
    public bool isEmpty
    {
        //make a property to access the slot clss
        get { return itemStack.Count == 0; }
    }

    public bool isAvailable
    {
        get { return currentItem.maxSize > itemStack.Count; }
    }

    private void RemoveItem()
    {
        //if item is in the slot
        if (!isEmpty)
        {
            Debug.Log(itemStack.Peek().gameObject.name + " is being deleted");
            //we discard the item by disassociating it on the stack
            itemStack.Pop();

            //if item number of items in one slot is greater than 1
            if (itemStack.Count > 1)
            {
                //display number of items in the slot
                stackText.text = itemStack.Count.ToString();
            }

            else
            {
                // do not display number of items since it is 1
                stackText.text = string.Empty;
            }


            if (isEmpty)
            {
                //if we used the last item in inventory, we'll replace it with the standard slot Sprite
                UpdateSprite(emptyS, highlightS);

                //increase number of blankSlots
                Inventory.BlankSlots++;
            }
        }

        gameObject.GetComponentInParent<Inventory>().isDeleting = false;
    }

    private void isDeleting()
    {
        if (gameObject.GetComponentInParent<Inventory>().isDeleting == true)
            gameObject.GetComponentInParent<Inventory>().isDeleting = false;
        else
            gameObject.GetComponentInParent<Inventory>().isDeleting = true;
    }
}