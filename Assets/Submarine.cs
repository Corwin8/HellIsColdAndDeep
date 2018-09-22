using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour {

	Rigidbody rigidBody;
	AudioSource propellerAudioSource;
	[SerializeField] float nozzleRotation = 75f;
	[SerializeField] float propellerStrenght = 1000f;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		propellerAudioSource = GetComponent<AudioSource>(); 
	}
	
	// Update is called once per frame
	void Update () {
		Maneuvering();
		PropellerSound();

	}

	private void Maneuvering()
	{
		float strenghtThisFrame = propellerStrenght*Time.deltaTime;
		float rotationThisFrame = nozzleRotation * Time.deltaTime;

		if (Input.GetKey(KeyCode.Space))
		{
			rigidBody.AddRelativeForce(Vector3.up*strenghtThisFrame);
		}

		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.forward*rotationThisFrame);
		}

		else if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(-Vector3.forward*rotationThisFrame);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		switch (collision.gameObject.tag)
		{
			case "Friendly":
				//do nothing
				break;
			default:
				print("You were swallowed by the depths.");
				break;
		}		
	}

	private void PropellerSound()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			propellerAudioSource.Play();
		}
		else if (Input.GetKeyUp(KeyCode.Space))
		{
			propellerAudioSource.Stop();
		}
	}
}
