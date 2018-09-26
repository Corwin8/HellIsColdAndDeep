﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Submarine : MonoBehaviour
{

	Rigidbody rigidBody;
	AudioSource audioSource;

	[SerializeField] float nozzleRotation = 75f;
	[SerializeField] float propellerStrenght = 1000f;

	[SerializeField] AudioClip mainPropellerSFX;
	[SerializeField] AudioClip explosionSFX;
	[SerializeField] AudioClip successSFX;

	[SerializeField] ParticleSystem mainPropellerVFX;
	[SerializeField] ParticleSystem explosionVFX;
	[SerializeField] ParticleSystem successVFX;

	[SerializeField] float levelLoadDelay = 2f;

	enum State { Alive, Dying, Transcending, Cheating};
	State state = State.Alive;

	bool collisionsDisabled = false;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>(); 
	}

	// Update is called once per frame
	void Update()
	{
		if (Debug.isDebugBuild)
		{
			RespondToDebugKeys();
		}

		if (state == State.Alive || state == State.Cheating)
		{
			Maneuvering();
			PropellerSound();
		}

	}

	private void RespondToDebugKeys()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			LoadNextLevel();
		}
		else if (Input.GetKeyDown(KeyCode.C))
		{
			collisionsDisabled = !collisionsDisabled;
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
		if (state != State.Alive || collisionsDisabled)
		{
			return;
		}

		switch (collision.gameObject.tag)
		{
			case "Friendly":
				break;
			case "Finish":
				StartSuccessSequence();
				break;
			default:
				StartDeathSequence();
				break;
		}		
	}

	private void StartDeathSequence()
	{
		state = State.Dying;
		audioSource.Stop();
		mainPropellerVFX.Stop();
		audioSource.PlayOneShot(explosionSFX);
		explosionVFX.Play();
		Invoke("DeathRestart", levelLoadDelay);
	}

	private void StartSuccessSequence()
	{
		state = State.Transcending;
		audioSource.Stop();
		audioSource.PlayOneShot(successSFX, 1);
		successVFX.Play();
		Invoke("LoadNextLevel", levelLoadDelay);
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
			audioSource.PlayOneShot(mainPropellerSFX);
			mainPropellerVFX.Play();
		}
		else if (Input.GetKeyUp(KeyCode.Space))
		{
			audioSource.Stop();
			mainPropellerVFX.Stop();
		}
	}
}
