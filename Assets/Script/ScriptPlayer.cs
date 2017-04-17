using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayer : MonoBehaviour {

    int speed = 10;
    Vector3 back, forward, left, right;
	// Use this for initialization
	void Start () {
        back = new Vector3(0, 0, -0.5f);
        forward = new Vector3(0, 0, 0.5f);
        left = new Vector3(-0.5f, 0, 0);
        right = new Vector3(0.5f, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    void Movement()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.Translate(right * speed * Time.deltaTime);
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.Translate(left * speed * Time.deltaTime);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            transform.Translate(forward * speed * Time.deltaTime);
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            transform.Translate(back * speed * Time.deltaTime);
        }
    }
}
