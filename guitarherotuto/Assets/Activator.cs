using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {

	public KeyCode key;
	public bool active = false;
	GameObject note;
	SpriteRenderer sr;
	Color oldColor;
	public bool createMode;
	public GameObject spawnedNote;


	void Awake () {
		sr = GetComponent<SpriteRenderer> ();
	}



	void Start()
	{
		oldColor = sr.color;
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(key))
		{
			if (createMode) {
				Instantiate (spawnedNote, transform.position, Quaternion.identity);
			}
			else{
				StartCoroutine (Pressed ());
					
				if (active) {
					Destroy (note);
					AddScore ();
					active = false;
				}
			}
		}
		 
	}

	void AddScore ()
	{
		PlayerPrefs.SetInt ("Score", PlayerPrefs.GetInt ("Score") + 100);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		active = true;

		if (col.gameObject.tag == "Note") 
		{
			note = col.gameObject;	
		}
		
	}

	void OnTriggerExit2D(Collider2D col)
	{
		active = false;
	}


	IEnumerator Pressed()
	{
		sr.color = new Color (0, 0, 0);
		yield return new WaitForSeconds(0.05f);
		sr.color = oldColor;
	}
}
