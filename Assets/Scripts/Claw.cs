using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Claw : MonoBehaviour {

	public Transform origin;
	public float speed = 4f;
	public Gun gun;
	// add ref to scoremanager variable;

	private Vector3 target;
//	private int SadValue = 100;
	private GameObject childObject;
	private LineRenderer lineRenderer;
	private bool hitSad;
	private bool hitJoy;
	private bool retracting;

	//audio
	public AudioClip joySound;
	public AudioClip sadSound;

	//2D story
	public GameObject storyboard;
	public GameObject joyText;
	public GameObject sadText;
	private bool deployed;
	public bool isStorytelling;

	void Start ()
	{
//		story.SetActive(false);

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
			LaunchStory ();
			if (!isStorytelling) {
				gun.CollectedObject ();
				if (hitSad) {
					hitSad = false;
				}
				if (hitJoy) {
					hitJoy = false;
				}
				Destroy (childObject);
				gameObject.SetActive (false);
			}
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

		if (other.gameObject.CompareTag ("Sad")) {
			hitSad = true;
			AudioSource audio = GetComponent<AudioSource>();
			audio.PlayOneShot(sadSound);

			childObject = other.gameObject;
			other.transform.SetParent (this.transform);
		} 
		else if (other.gameObject.CompareTag ("Joy")) {
			hitJoy = true;
			AudioSource audio = GetComponent<AudioSource>();
			audio.PlayOneShot(joySound);
			childObject = other.gameObject;
			other.transform.SetParent (this.transform);
		} 
	}

	void LaunchStory ()
	{
		Debug.Log ("Launch Story");
		isStorytelling = true;
		if (hitJoy || hitSad) {
			if (hitJoy) {
				joyText.SetActive (true);
			} else if (hitSad) {
				sadText.SetActive (true);
			} 
			storyboard.SetActive (true);
			CloseStoryboard ();
		} else {
			isStorytelling = false;
		}

	}

	void CloseStoryboard ()
	{
		if (Input.GetKeyDown(KeyCode.Return)) {
			Debug.Log ("return is pressed");
			isStorytelling = false;
			storyboard.SetActive (false);
			joyText.SetActive (false);
			sadText.SetActive (false);
		}

	}
}
