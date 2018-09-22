using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Submarine : MonoBehaviour {

	Rigidbody rigidBody;
	AudioSource propellerAudioSource;
	[SerializeField] float nozzleRotation = 75f;
	[SerializeField] float propellerStrenght = 1000f;
	enum State { Alive, Dying, Transcending};
	State state = State.Alive;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		propellerAudioSource = GetComponent<AudioSource>(); 
	}

	// Update is called once per frame
	void Update() {
		if (state == State.Alive)
		{
			Maneuvering();
			PropellerSound();
		}
		else
		{
			propellerAudioSource.Stop();
		}

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
		if (state != State.Alive)
		{
			return;
		}

		switch (collision.gameObject.tag)
		{
			case "Friendly":
				break;
			case "Finish":
				state = State.Transcending;
				Invoke("LoadNextLevel", 2f);
				break;
			default:
				state = State.Dying;
				Invoke("DeathRestart", 2f);
				break;
		}		
	}

	private void DeathRestart()
	{
		SceneManager.LoadScene(0);
	}

	private void LoadNextLevel()
	{
		SceneManager.LoadScene(1);
	}

	private void OnTriggerEnter(Collider other)
	{
		print("Trigger entered.");
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
