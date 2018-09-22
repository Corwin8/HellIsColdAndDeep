using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour {

	Rigidbody rigidBody;
	AudioSource propellerAudioSource;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		propellerAudioSource = GetComponent<AudioSource>(); 
	}
	
	// Update is called once per frame
	void Update () {
		ProcessInput();
		
	}

	private void ProcessInput()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			rigidBody.AddRelativeForce(Vector3.up);
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			propellerAudioSource.Play();
		}
		else if (Input.GetKeyUp(KeyCode.Space))
		{
			propellerAudioSource.Stop();
		}

		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.forward);
		}

		else if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(-Vector3.forward);
		}
	}
}
