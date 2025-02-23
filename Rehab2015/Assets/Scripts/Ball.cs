﻿using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
	public static BallType[] ballTypes = 
	{
		new BallType("CubeBall/CubeBall", "CubeBall/CubeBallSound", "CubeBall/CubeBallSound"),
		new BallType("CylinderBall/CylinderBall", "CylinderBall/CylinderBallSound", "CylinderBall/CylinderBallSound")
	};

	public BallType BallType;

	private Vector3 gravity = new Vector3(0, -0.0075f, 0);

	private Vector3 velocity = new Vector3(0, 0, -0.04f);
	private Quaternion tumble = Quaternion.AngleAxis(1.0f, new Vector3(0.5f, 0.3f, 0.7f));

	// Use this for initialization
	void Start ()
	{
		string modelName;

		//if (Random.value < 0.5)
		//{
		//	modelName = "CubeBall/CubeBall";
		//}
		//else
		//{
		//	modelName = "CylinderBall/CylinderBall";
		//}

		int ballTypeIndex = (int)(Random.value * ballTypes.Length);
		BallType = ballTypes[ballTypeIndex];

		GameObject modelInstance = Instantiate(Resources.Load(BallType.ModelName, typeof(GameObject))) as GameObject;
		modelInstance.transform.SetParent(this.transform, false);

		//GetComponentInParent<AudioSource>().PlayOneShot(    )
		playLaunchSound();
	}

	// Update is called once per frame
	void Update () {

		Vector3 gravityDelta = gravity * Time.deltaTime;

		//velocity = velocity + new Vector3(0, -0.00015f, 0);
		velocity = velocity + gravityDelta;
		velocity = velocity * 0.999f;

		//this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.04f);
		this.transform.position = this.transform.position + velocity;

		this.transform.Rotate(tumble.eulerAngles);

		if (transform.position.z < -2)
		{
			Destroy(this.gameObject);
		}
	}

	public void setVelocity(Vector3 velocity)
	{
		this.velocity = velocity;
	}

	public void playLaunchSound()
	{
		GetComponent<AudioSource>().PlayOneShot(BallType.launchAudioClip);
	}

	public void playCatchSound()
	{
		GetComponent<AudioSource>().PlayOneShot(BallType.catchAudioClip);
	}
}

