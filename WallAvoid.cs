using UnityEngine;
using System.Collections;

public class WallAvoid : MonoBehaviour
{

	// Length of the ray
	public int rayLength;

	// Structure used to get information back from a raycast.
	public RaycastHit hit;

	//string layerName;
	int layerMask;

	public bool boolCheck;

	Enemy me;

	Vector3 direction;

	public Transform originPoint;

	public float rotateValue;
	public Vector3 targetRotation;

	public bool isRotating;
	bool isUpdate;

	float yVal;

	// Use this for initialization
	void Start () 
	{
		me = GetComponent<Enemy> ();
		boolCheck = false;
		isRotating = false;
		isUpdate = true;
		layerMask = LayerMask.NameToLayer ("AI");

		yVal = 0f;

	}
	
	// Update is called once per frame
	void Update () 
	{
		// The direction for the ray
	//	direction = originPoint.TransformDirection(Vector3.forward);



		direction = me.velocity + me.transform.position + (transform.forward * 2);
		
		// Displaying the ray in the desired direction and position
		Debug.DrawLine(me.transform.position, direction, Color.red);

		if (isUpdate)
		{
			if (Physics.Raycast (transform.position, direction, out hit, rayLength)) 
			{
				if (hit.transform.gameObject.layer == layerMask) 
				{
					boolCheck = true;
					me.isAvoidWall = true;
					CheckLayer (boolCheck);
				} 
				else 
				{
					boolCheck = false;
					CheckLayer (boolCheck);
				}
			}
		}
			
		/*	if(isRotating)
		{
			
		}*/
	}

	void CheckLayer(bool hasHit)
	{		
		if (hasHit) 
		{
			isRotating = false;
			isUpdate = false;
			SetRotation ();
		}
	}

	void SetRotation()
	{
		CheckY ();

		//me.angular += new Vector3(0,yVal,0);

		//me.transform.eulerAngles = new Vector3 (0, yVal, 0);

		Debug.Log ("me.transform.eulerAngles : " + me.transform.eulerAngles);

		rotateValue = Random.Range (90f,180f);

		Debug.Log ("Random value generated : " + rotateValue);

		targetRotation = new Vector3 (0, rotateValue,0);

		Debug.Log ("Vector3 RotateV : "+targetRotation);

		Debug.Log ("me.transform.rotation.eulerAngles " + me.transform.rotation.eulerAngles + " - RotateV " + targetRotation);

//		rotateV = me.transform.rotation.eulerAngles - rotateV;
		targetRotation = targetRotation - me.transform.rotation.eulerAngles;

		Debug.Log ("New Vector3 RotateV after subtraction: "+targetRotation);


		//me.transform.eulerAngles = targetRotation;
		me.angular += targetRotation;
		me.tempRotation += targetRotation;


		Debug.Log ("Temporary Rotation : " + me.tempRotation);

		CheckY ();

		Debug.Log ("Passing RotateV value to eulerAngles: " + me.transform.rotation.eulerAngles);

		CheckY ();
	
		isRotating = true;

		//Debug.Log ("transform.localeulerangles : " + me.transform.localEulerAngles); 
	}

	// To check and calculate the value of y-rotation if negative or positive.
	void CheckY()
	{

		if (me.transform.rotation.eulerAngles.y > 180) {		
			yVal = me.transform.rotation.eulerAngles.y - 360f;
			Debug.Log ("Y (if negative): " + yVal);
		}
		else 
		{
			yVal = me.transform.rotation.eulerAngles.y;
			Debug.Log ("Y (if positive): " + yVal);
		}

	}
}

