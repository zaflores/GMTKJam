using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTextEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (transform.localScale.x <= 6f)
	    {
	        transform.localScale += transform.localScale * Time.deltaTime * 2;
	    }
	}

    public void DESTRUCTOIN()
    {
        Destroy(gameObject);
    }
}
