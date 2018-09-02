using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBubbles : MonoBehaviour {

	// Use this for initialization
    private float DEATHTIMER = 1.0f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (DEATHTIMER >= .4f)
	    {
	        transform.localScale += new Vector3(2.0f * Time.deltaTime, 2.0f * Time.deltaTime, 2.0f * Time.deltaTime);
        }
        DEATHTIMER -= Time.deltaTime;
	    if (DEATHTIMER <= 0)
	    {
            Destroy(gameObject);
	    }
	}
}
