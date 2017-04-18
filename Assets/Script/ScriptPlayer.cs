using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayer : MonoBehaviour {

    public Rigidbody Meteor;
    float buttonPressDelay = 0.5f, buttonPressCount = 0, buttonPressFirstTime, speed, movementSpeed = 10, force = 10;
    Vector3 back, forward, left, right;
    Rigidbody rigidbodyCharacter;
    bool canJump;

    // Use this for initialization
	void Start () {
        back = new Vector3(0, 0, -0.5f);
        forward = new Vector3(0, 0, 0.5f);
        left = new Vector3(-0.5f, 0, 0);
        right = new Vector3(0.5f, 0, 0);
        speed = movementSpeed;
        rigidbodyCharacter = GetComponent<Rigidbody>(); 
	}

    void FixedUpdate()
    {
        if (canJump)
        {
            canJump = false;
            rigidbodyCharacter.AddForce(0, force, 0, ForceMode.Impulse);
        }
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
        Behavior();
	}

    void Movement()
    {
        if (Input.anyKeyDown)
        {
            buttonPressCount++;
            Debug.Log(buttonPressCount);
            //Debug.Log(Time.time + " - " + buttonPressFirstTime + "(" + (Time.time - buttonPressFirstTime) + ")" + " > " + buttonPressDelay);
            if (buttonPressCount >= 2 && Time.time - buttonPressFirstTime < buttonPressDelay)
            {
                speed = movementSpeed * 2;
            }
            else
            {
                speed = movementSpeed;
            }
            buttonPressFirstTime = Time.time;
        }
        else if (!Input.anyKeyDown && buttonPressCount > 2 || Time.time - buttonPressFirstTime > buttonPressDelay)
        {
            buttonPressCount = 0;
        }

        if (Input.GetButtonDown("Jump"))
        {
            canJump = true;
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.Translate(right * speed * Time.deltaTime);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.Translate(left * speed * Time.deltaTime);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            transform.Translate(forward * speed * Time.deltaTime);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            transform.Translate(back * speed * Time.deltaTime);
        }
    }

    void Behavior()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnMeteor();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            FireMeteor();
        }
    }

    void SpawnMeteor()
    {
        Rigidbody spawnMeteor = (Rigidbody)Instantiate(Meteor, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z + 10), transform.rotation);
        spawnMeteor.velocity = transform.up * -speed;
    }
    void FireMeteor()
    {
        Rigidbody spawnMeteor = (Rigidbody)Instantiate(Meteor, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), transform.rotation);
        spawnMeteor.velocity = transform.forward * speed;
    }
}
