using UnityEngine;
using System.Collections;

public class ArrowPhysics : MonoBehaviour {
    public Vector3 velocity;
    //private Vector3[] curvingUnit;

    public float x, y, z;
    private float g = 9.81f;
    public float[] t = new float[5];
    public float h;
    public float y0;
    public float x0;
    public float Theta = 45;
    private float u0;
    private float Range;
    private float myForce;

    void Start()
    {
        for (int i =0; i<t.Length;i++ )
        {
            t[i] = i;
        }


    }
    void Update()
    {




        gameObject.transform.Translate(x * Time.deltaTime, y * Time.deltaTime, z * Time.deltaTime);

    }

    private void HeightCalc()
    {

        h = ((u0 * u0) * Mathf.Sin(Theta)) / 2f * g;

    }
    private void xDisplacement()
    {
        //ti

    }



}

