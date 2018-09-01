﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteManager : MonoBehaviour {

    public int types = 5;
    public Sprite[] sprites1;
    public Sprite[] sprites2;

    public float animationTime = 1;
    private float timeTracker = 0;

    //variable that stores this object's type
    private int type;

    //variable for object's sprite renderer
    private SpriteRenderer render;

    // Use this for initialization
    void Start ()
    {
        type = gameObject.GetComponent<Tile>().GetNumberType();
        render = gameObject.GetComponent<SpriteRenderer>();

        //set object's sprite to correct type
        render.sprite = sprites1[type];
    }
	
	// Update is called once per frame
	void Update ()
    {
        timeTracker -= Time.deltaTime;

        if (timeTracker <= 0)
        {
            timeTracker = animationTime;

            //switch sprites
            if (render.sprite == sprites1[type])
            {
                render.sprite = sprites2[type];
            }

            else
            {
                render.sprite = sprites1[type];
            }
        }
	}
}
