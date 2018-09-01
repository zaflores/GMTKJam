using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class revolt : MonoBehaviour {

    public int rows;
    public int cols;
    private GameObject[,] allObjects;
    private BoardManager boardManager;
    public Sprite[] rioters;

    public float animationTime = 1;
    private float timeTracker = 0;

    public GameObject revoltImage;

    private bool revolting = false;

    public void startRiot()
    {
        StartCoroutine(riotCoroutine());
    }

    private IEnumerator riotCoroutine()
    {
        allObjects = boardManager.getBoard();
        //make all people rioters
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                allObjects[i, j].GetComponent<SpriteRenderer>().sprite = rioters[0];
                spriteManager tempSpriteManager = allObjects[i, j].GetComponent<spriteManager>();
                tempSpriteManager.Riot();
                allObjects[i, j].GetComponent<BoxCollider2D>().enabled = false;
                yield return new WaitForSeconds(0.1f);
            }
        }
        Instantiate(revoltImage, allObjects[rows / 2, cols / 2].transform);
    }


    // Use this for initialization
    void Start ()
    {
        boardManager = GetComponent<BoardManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {/*
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
        */
    }


}
