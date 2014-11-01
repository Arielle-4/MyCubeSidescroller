using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {

	public static float distanceTraveled;

	public float acceleration;

	private bool touchingPlatform;


	public Vector3 jumpVelocity;

	public float gameOverY;

	void Update () {

		if(touchingPlatform && Input.GetButtonDown("Jump")){
			rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);

			touchingPlatform = false;
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
