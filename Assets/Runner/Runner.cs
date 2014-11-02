using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {

	public static float distanceTraveled;

	public float acceleration;

	private bool touchingPlatform;


	public Vector3 boostVelocity, jumpVelocity;

	public float gameOverY;

	private Vector3 startPosition;
	
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition = transform.localPosition;
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
	}


	private static int boosts;

	private void GameStart () {
		boosts = 0;
		distanceTraveled = 0f;
		transform.localPosition = startPosition;
		renderer.enabled = true;
		rigidbody.isKinematic = false;
		enabled = true;
	}

	public static void AddBoost () {
		boosts += 1;
	}
	
	private void GameOver () {
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
	}

	void Update () {

		if(touchingPlatform && Input.GetButtonDown("Jump")){

			if(touchingPlatform){

			rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);

			touchingPlatform = false;
			}

			else if(boosts > 0){
				rigidbody.AddForce(boostVelocity, ForceMode.VelocityChange);
				boosts -= 1;
			}
		}

	
		distanceTraveled = transform.localPosition.x;

		if(transform.localPosition.y < gameOverY){
			GameEventManager.TriggerGameOver();
		}
	}
 
	void FixedUpdate () {
		if(touchingPlatform){
			rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
		}
	}
	
	void OnCollisionEnter () {
		touchingPlatform = true;
	}
	
	void OnCollisionExit () {
		touchingPlatform = false;
	}


}
