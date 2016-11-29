using UnityEngine;
using System.Collections;

public class DestroyForNonMobile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		#if UNITY_EDITOR 
		Destroy (this.gameObject);
		#endif 
	}
}
