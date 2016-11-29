using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour {

	CharacterControl me;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () 
	{
		if(me == null)
		{
			me = FindObjectOfType<CharacterControl>();
			//adding the Punch function in the OnClick() in Inspector
			GetComponent<Button>().onClick.AddListener(me.Punch);
		}
	}
}
