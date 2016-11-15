using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

    public GameObject spawner;
    public float minSpeed, maxSpeed, finalXPos = -160;
    float speed;

	// Use this for initialization
	void Start ()
    {
        spawner = GameObject.Find("Spawner");
        RandomizeSpeed();
	}

    void RandomizeSpeed()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Restart()
    {
        float y = Random.Range(-30, 30);
        float z = Random.Range(-30, 30);

        Vector3 newPosition = new Vector3(spawner.transform.position.x, spawner.transform.position.y + y, spawner.transform.position.z + z);
        transform.position = newPosition;
        RandomizeSpeed();
    }

	// Update is called once per frame
	void Update ()
    {
        if (transform.position.x <= finalXPos)
            Restart();

        transform.Translate(Vector3.right * speed * Time.deltaTime);    
	}

}
