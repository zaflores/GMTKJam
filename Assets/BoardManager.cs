using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    [SerializeField] private Sprite[] _gameSprites;
    [SerializeField] private GameObject _titlePrefab;
    [SerializeField] private int _xTitles;
    [SerializeField] private int _yTitles;

    private GameObject[][] board;

	void Start ()
	{
	    Vector2 offset = _titlePrefab.GetComponent<SpriteRenderer>().bounds.size;
        CreateBoard(offset.x,offset.y);
    }

    private void CreateBoard(float offsetX, float offsetY)
    {
        Vector2 startingPos = transform.position;
        for (int i = 0; i < _xTitles; i++)
        {
            for (int j = 0; j < _yTitles; j++)
            {
                Vector2 positionToSpawn = new Vector2(startingPos.x + offsetX * i, startingPos.y + offsetY * j);
                GameObject title = Instantiate(_titlePrefab, positionToSpawn,Quaternion.identity);
                board[i][j] = title;
            }
        }
    }
}
