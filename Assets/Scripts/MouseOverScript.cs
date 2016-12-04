using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseOverScript : MonoBehaviour {

    public Text HelpText;
    private string TextInfo;
    private Item myItem;
    public GameObject PointedAt;
    private GameObject TextHolder;
    private Slot slotScr;


    private void Start()
    {
        slotScr = gameObject.GetComponent<Slot>();
        PointedAt = gameObject.GetComponent<Slot>().currentItemA;
        TextHolder = GameObject.FindGameObjectWithTag("HelpText");
        HelpText = TextHolder.GetComponent<Text>();

    }
	public void OnMouseEnter()
    {
        if (PointedAt == null)
        {
            PointedAt = slotScr.currentItemA;
        }
        if (PointedAt != null)
        { 
            TextInfo = slotScr.currentItemA.GetComponent<Item>().flavorText;
            HelpText.text = TextInfo;
        }
        //Debug.Log("MouseEnter");

    }

    public void OnMouseExit()
    {

        HelpText.text = " ";
        //Debug.Log("MouseExit");

    }
}
