using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revolt : MonoBehaviour {

    public int rows;
    public int cols;
    public GameObject[,] allObjects;
    public Sprite[] rioters;

    public float animationTime = 1;
    private float timeTracker = 0;

    public GameObject revoltImage;

    private bool revolting = false;

    public void startRiot()
    {
        //make all people rioters
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                allObjects[i, j].GetComponent<SpriteRenderer>().sprite = rioters[0];
            }
        }

        Instantiate(revoltImage, allObjects[rows/2, cols/2].transform);

        revolting = true;
    }


    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (revolting)
        {
            //animation for rioters
            timeTracker -= Time.deltaTime;

            if (timeTracker <= 0)
            {
                timeTracker = animationTime;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (allObjects[i, j].GetComponent<SpriteRenderer>().sprite == rioters[0])
                        {

                            allObjects[i, j].GetComponent<SpriteRenderer>().sprite = rioters[1];
                        }

                        else
                        {
                            allObjects[i, j].GetComponent<SpriteRenderer>().sprite = rioters[0];
                        }
                    }
                }
            }
        }
    }


}
