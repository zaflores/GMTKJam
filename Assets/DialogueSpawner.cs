using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject[] ThingsToSay;
    private List<GameObject> gameObjectsToSpeak = null;
    //[SerializeField] private G
    private float timer = 10.0f;
    private float timerTracker = 3.0f;

    private GameObject spawnedObject = null;
	// Use this for initialization
	void Start () {
	}

    public void refreshList()
    {
        if (gameObjectsToSpeak == null)
        {
            gameObjectsToSpeak = new List<GameObject>();
        }
        if (gameObjectsToSpeak.Count != 0)
        {
            gameObjectsToSpeak.Clear();
        }
        for (int i = 0; i < ThingsToSay.Length; i++)
        {
            gameObjectsToSpeak.Add(ThingsToSay[i]);
        }
        timerTracker = 3.0f;
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
        }
    }
	
	// Update is called once per frame
	void Update ()
	{
	    timerTracker -= Time.deltaTime;
	    if (timerTracker <= 0)
	    {
	        int Index = Random.Range(0, gameObjectsToSpeak.Count);
            Vector3 toSpawn = (transform.position + new Vector3(3.2f,.5f,-6));
	        spawnedObject = Instantiate(gameObjectsToSpeak[Index], toSpawn, transform.rotation);
	        spawnedObject.transform.parent = gameObject.transform;
	        gameObjectsToSpeak.Remove(spawnedObject);
            Destroy(spawnedObject,10.0f);
	        timer = Random.Range(50.0f, 70.0f);
	        timerTracker = timer;
	    }
	}
    
}
