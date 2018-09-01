using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class riotText : MonoBehaviour {

    public float growthRate;
    private float growthTimer;
    public float maxTime;
    public int spinSpeed;
    bool growing = true;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (growthTimer >= maxTime)
        {
            growing = false;
        }

        else if (growthTimer <= 0)
        {
            growing = true;
        }

        if (growthTimer < maxTime && growing)
        {
            transform.localScale += new Vector3(growthRate * Time.deltaTime, growthRate * Time.deltaTime, 0);
            growthTimer += Time.deltaTime;
        }

        else
        {
            transform.localScale -= new Vector3(growthRate * Time.deltaTime, growthRate * Time.deltaTime, 0);
            growthTimer -= Time.deltaTime;
        }
      
        gameObject.GetComponent<Transform>().Rotate(Vector3.back * Time.deltaTime * spinSpeed);
    }
}
