using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayer : MonoBehaviour {

    float speed, buttonPressCounter, buttonPressFirstTime;
    public float jumpPower = 10, buttonPressDelay = 0.5f, movementSpeed = 10;
    bool isJumping, isButtonDoublePressed = false;
    Rigidbody rigdbody;
    Vector3 back, forward, left, right;
    TextMesh logText;

	// Use this for initialization

	void Start () {
        back = new Vector3(0, 0, -0.5f);
        forward = new Vector3(0, 0, 0.5f);
        left = new Vector3(-0.5f, 0, 0);
        right = new Vector3(0.5f, 0, 0);
        speed = movementSpeed;

        rigdbody = this.GetComponent<Rigidbody>();

        logText = GameObject.Find("LogText").gameObject.GetComponent<TextMesh>();
	}

    void FixedUpdate()
    {

        if(isJumping)
        {
            isJumping = false;
            rigdbody.AddForce(0, jumpPower, 0, ForceMode.Impulse);
        }
    }

	// Update is called once per frame
	void Update () {
        Movement();
        logText.text = movementSpeed.ToString();
	}

    void Movement()
    {

        if(buttonPressCounter > 1)
        {
            Debug.Log("you press me twice");
            speed = movementSpeed * 2;
            isButtonDoublePressed = true;
        }
        else
        {
            speed = movementSpeed;
        }

        if (!Input.anyKey && isButtonDoublePressed == true)
        {
            Debug.Log("I have to reset you!");
            buttonPressCounter = 0;
            isButtonDoublePressed = false;
        }

        if (Input.anyKeyDown && isButtonDoublePressed == false && Time.time - buttonPressFirstTime < buttonPressDelay)
        {
            buttonPressCounter++;
            Debug.Log(buttonPressCounter);
        }

        if (Input.GetButtonDown("Jump"))
        {

            isJumping = true;
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            buttonPressFirstTime = Time.time;
            transform.Translate(right * speed * Time.deltaTime);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            buttonPressFirstTime = Time.time;
            transform.Translate(left * speed * Time.deltaTime);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            buttonPressFirstTime = Time.time;
            transform.Translate(forward * speed * Time.deltaTime);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            buttonPressFirstTime = Time.time;
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
