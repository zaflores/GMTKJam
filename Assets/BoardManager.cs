using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    [SerializeField] private Sprite[] _gameSprites;
    [SerializeField] private GameObject _titleSmallPrefab;
    [SerializeField] private GameObject _titleNormalPrefab;
    [SerializeField] private int _xTitles;
    [SerializeField] private int _yTitles;
    [SerializeField] private Vector2 _startPosForGameBoard;
    private GameObject[,] masterBoard;
    private GameObject[,] gameBoard;
    private int[] numOfEachSprite;
	void Start ()
	{
	    numOfEachSprite = new int[_gameSprites.Length];
        masterBoard = new GameObject[_xTitles,_yTitles];
	    gameBoard = new GameObject[_xTitles, _yTitles];
        Vector2 offset = _titleSmallPrefab.GetComponent<SpriteRenderer>().bounds.size;
        CreateInitialBoard(offset.x + 0.5f,offset.y + 0.5f);
	    offset = _titleNormalPrefab.GetComponent<SpriteRenderer>().bounds.size;
        CreateGameBoard(offset.x + 0.5f,offset.y + 0.5f);
    }

    private void CreateInitialBoard(float offsetX, float offsetY)
    {
        Vector2 startingPos = transform.position;
        for (int i = 0; i < _xTitles; i++)
        {
            for (int j = 0; j < _yTitles; j++)
            {
                Vector2 positionToSpawn = new Vector2(startingPos.x + offsetX * i, startingPos.y + offsetY * j);
                GameObject title = Instantiate(_titleSmallPrefab, positionToSpawn,Quaternion.identity);
                masterBoard[i,j] = title;
                title.transform.parent = transform;
                int spriteToChoose = Random.Range(0, _gameSprites.Length);
                title.GetComponent<SpriteRenderer>().sprite = _gameSprites[spriteToChoose];
                numOfEachSprite[spriteToChoose]++;
            }
        }
    }

    private void CreateGameBoard(float offsetX, float offsetY)
    {
        for (int i = 0; i < _xTitles; i++)
        {
            for (int j = 0; j < _yTitles; j++)
            {
                Vector2 positionToSpawn = new Vector2(_startPosForGameBoard.x + offsetX * i, _startPosForGameBoard.y + offsetY * j);
                GameObject title = Instantiate(_titleNormalPrefab, positionToSpawn, Quaternion.identity);
                gameBoard[i, j] = title;
                title.transform.parent = transform;
                int spriteToChoose = Random.Range(0, _gameSprites.Length );
                while (numOfEachSprite[spriteToChoose] == 0)
                {
                    spriteToChoose = Random.Range(0, _gameSprites.Length );
                }
                title.GetComponent<SpriteRenderer>().sprite = _gameSprites[spriteToChoose];
                numOfEachSprite[spriteToChoose]--;
            }
        }
    }
}
