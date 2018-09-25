using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour {

	[SerializeField] Vector3 movementVector;
	float movementFactor;
	Vector3 startingPos;
	[SerializeField] float period = 4f;

	// Use this for initialization
	void Start () {
		startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (period <= Mathf.Epsilon)
		{
			Debug.Log("Period cannot be 0. (dividing by zero)");
			return;
		}

		float cycles = Time.time / period;
		const float tau = Mathf.PI * 2f;
		float rawSinWave = Mathf.Sin(cycles * tau);

		movementFactor = rawSinWave;

		Vector3 offset = movementVector * movementFactor;
		transform.position = startingPos + offset;
		
	}
}
