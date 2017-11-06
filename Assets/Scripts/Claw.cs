using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Claw : MonoBehaviour {

	public Transform origin;
	public float speed = 4f;
	public Gun gun;
	// add ref to scoremanager variable;

	private Vector3 target;
//	private int jewelValue = 100;
	private GameObject childObject;
	private LineRenderer lineRenderer;
	private bool hitJewel;
	private bool retracting;

	//audio
	public AudioClip joySound;
	public AudioClip sadSound;


	void Start ()
	{

	}

	void Awake () {
		lineRenderer = GetComponent<LineRenderer>();

	}
	
	// Update is called once per frame
	void Update ()
	{
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, target, step);
		lineRenderer.SetPosition (0, origin.position);
		lineRenderer.SetPosition (1, transform.position);
		if (transform.position == origin.position && retracting) {
			gun.CollectedObject ();
			if (hitJewel) {
				hitJewel = false;

			}

			Destroy(childObject);
			LaunchStory ();
			gameObject.SetActive(false);
		}
		
	}

	public void ClawTarget (Vector3 pos)
	{
		target = pos;

	}

	void OnTriggerEnter (Collider other)
	{
		retracting = true;
		target = origin.position;

		if (other.gameObject.CompareTag ("Jewel")) {
			hitJewel = true;
			AudioSource audio = GetComponent<AudioSource>();
			audio.PlayOneShot(sadSound);

			childObject = other.gameObject;
			other.transform.SetParent (this.transform);
		} 
		else if (other.gameObject.CompareTag ("Rock")) {
			hitJewel = true;
			AudioSource audio = GetComponent<AudioSource>();
			audio.PlayOneShot(joySound);
			childObject = other.gameObject;
			other.transform.SetParent (this.transform);
		} 
	}

	void LaunchStory ()
	{
		Debug.Log("Launch Story");

	}
}
