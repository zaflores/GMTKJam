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

    public GameObject revoltImage;

    private AudioSource audioSource;
    [SerializeField] private AudioSource songSource;
    public void startRiot()
    {
        StartCoroutine(riotCoroutine());
    }

    private IEnumerator riotCoroutine()
    {
        allObjects = boardManager.getBoard();
        songSource.Stop();
        audioSource.Play();
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
        GetComponent<barFill>().ActivateUI();
    }


    // Use this for initialization
    void Start ()
    {
        boardManager = GetComponent<BoardManager>();
        audioSource = GetComponent<AudioSource>();
    }
	// Update is called once per frame
}
