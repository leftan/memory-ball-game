using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
	public GameObject claw;
	public bool isShooting;
	public Animator minerAnimator;
	public Claw clawScript;

	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown("space") && !isShooting) 
		{
			LaunchClaw ();
		}
		
	}

	void LaunchClaw ()
	{
		isShooting = true;
		minerAnimator.speed = 0;
		RaycastHit hit;
		Vector3 up = transform.TransformDirection (Vector3.up);

		if (Physics.Raycast (transform.position, up, out hit, 100)) {
			claw.SetActive(true);
			clawScript.ClawTarget(hit.point);
		}
	}


	public void CollectedObject ()
	{
		isShooting = false;
		minerAnimator.speed = 1;

	}
}
