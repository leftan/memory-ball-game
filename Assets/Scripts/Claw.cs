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

	private bool hitSadStory1;
	private bool hitSadStory2;
	private bool hitSadStory3;

	private bool hitJoyStory1;
	private bool hitJoyStory2;
	private bool retracting;

	//audio
	public AudioClip joySound;
	public AudioClip joySound2;
	public AudioClip sadSound;
	public AudioClip sadSound2;
	public AudioClip sadSound3;

	//2D story
	public GameObject storyboard;
	public GameObject joyText;
	public GameObject joyText2;
	public GameObject sadText;
	public GameObject sadText2;
	public GameObject sadText3;

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
					hitSadStory1 = false;
					hitSadStory2 = false;
					hitSadStory3 = false;
				}
				if (hitJoy) {
					hitJoy = false;
					hitJoyStory1 = false;
					hitJoyStory2 = false;

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
			hitSadStory1 = true;
			AudioSource audio = GetComponent<AudioSource>();
			audio.PlayOneShot(sadSound);
			childObject = other.gameObject;
			other.transform.SetParent (this.transform);
		} 
		else if (other.gameObject.CompareTag ("Joy")) {
			hitJoy = true;
			hitJoyStory1 = true;
			Debug.Log("hit joy story 1");
			AudioSource audio = GetComponent<AudioSource>();
			audio.PlayOneShot(joySound);
			childObject = other.gameObject;
			other.transform.SetParent (this.transform);
		} else if (other.gameObject.CompareTag ("Joy2")) {
			hitJoy = true;
			hitJoyStory2 = true;
			Debug.Log("hit joy story 2");
			AudioSource audio = GetComponent<AudioSource>();
			audio.PlayOneShot(joySound2);
			childObject = other.gameObject;
			other.transform.SetParent (this.transform);
		} else if (other.gameObject.CompareTag ("Sad2")) {
			hitSad = true;
			hitSadStory2 = true;
			AudioSource audio = GetComponent<AudioSource>();
			audio.PlayOneShot(sadSound2);
			childObject = other.gameObject;
			other.transform.SetParent (this.transform);
		} else if (other.gameObject.CompareTag ("Sad3")) {
			hitSad = true;
			hitSadStory3 = true;
			AudioSource audio = GetComponent<AudioSource>();
			audio.PlayOneShot(sadSound3);
			childObject = other.gameObject;
			other.transform.SetParent (this.transform);
		} 
	}

	void LaunchStory ()
	{
		
		isStorytelling = true;
		if (hitJoy || hitSad) {
			if (hitJoy) {
				if (hitJoyStory2) {
					joyText2.SetActive (true);
				} 
				if (hitJoyStory1) {
					joyText.SetActive (true);
				}
			} else if (hitSad) {
				if (hitSadStory2) {
					sadText2.SetActive (true);
				} 
				if (hitSadStory3) {
					sadText3.SetActive (true);
				} 

				if (hitSadStory1) {
					sadText.SetActive (true);
				}
			} 
			// storyboard.SetActive (true);
			CloseStoryboard ();
		} else {
			isStorytelling = false;
		}

	}

	void CloseStoryboard ()
	{
		if (Input.GetKeyDown(KeyCode.Return)) {
			isStorytelling = false;
			// storyboard.SetActive (false);
			joyText.SetActive (false);
			joyText2.SetActive (false);
			sadText.SetActive (false);
			sadText2.SetActive (false);
			sadText3.SetActive (false);
		}

	}
}
