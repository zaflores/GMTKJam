using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{

    [SerializeField] private Sprite[] _gameSprites;
    [SerializeField] private GameObject _titleSmallPrefab;
    [SerializeField] private GameObject _titleNormalPrefab;
    [SerializeField] private int _xTitles;
    [SerializeField] private int _yTitles;
    [SerializeField] private Vector2 _startPosForGameBoard;
    private int[,] masterBoard;
    private List<GameObject> holdingMasterObjects;
    private GameObject[,] gameBoard;
    private int[] numOfEachSprite;

    public GameObject[,] getBoard()
    {
        return gameBoard;
    }

    public void StartBoardGame()
    {
        holdingMasterObjects = new List<GameObject>();
        numOfEachSprite = new int[_gameSprites.Length];
        masterBoard = new int[_xTitles, _yTitles];
        gameBoard = new GameObject[_xTitles, _yTitles];
        Vector2 offset = _titleSmallPrefab.GetComponent<SpriteRenderer>().bounds.size;
        CreateInitialBoard(offset.x + 0.5f, offset.y + 0.5f);
        offset = _titleNormalPrefab.GetComponent<SpriteRenderer>().bounds.size;
        CreateGameBoard(offset.x + 0.5f, offset.y + 0.5f);
    }

    public int CheckForThrees()
    {
        int numThrees = 0;
        //check for vertical 3's
        for (int i = 0; i < _xTitles; i++)
        {
            for (int j = 1; j < _yTitles - 1; j++)
            {
                Tile tempTile1 = gameBoard[i, j].GetComponent<Tile>();
                Tile tempTile2 = gameBoard[i, j + 1].GetComponent<Tile>();
                Tile tempTile3 = gameBoard[i, j - 1].GetComponent<Tile>();
                if (tempTile1.GetNumberType() == tempTile2.GetNumberType() &&
                    tempTile1.GetNumberType() == tempTile3.GetNumberType())
                {
                    numThrees++;
                    break;
                }
            }
        }

        for (int i = 0; i < _yTitles; i++)
        {
            for (int j = 1; j < _xTitles - 1; j++)
            {
                Tile tempTile1 = gameBoard[j, i].GetComponent<Tile>();
                Tile tempTile2 = gameBoard[j + 1, i].GetComponent<Tile>();
                Tile tempTile3 = gameBoard[j - 1, i].GetComponent<Tile>();
                if (tempTile1.GetNumberType() == tempTile2.GetNumberType() &&
                    tempTile1.GetNumberType() == tempTile3.GetNumberType())
                {
                    numThrees++;
                    break;
                }
            }
        }
        return numThrees;
    }
    public bool CheckForWin()
    {
        for (int i = 0; i < _xTitles; i++)
        {
            for (int j = 0; j < _yTitles; j++)
            {
                Tile tempTile = gameBoard[i, j].GetComponent<Tile>();
                if (tempTile.GetNumberType() != masterBoard[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void ClearGame()
    {
        ClearGameBoard();
        ClearMasterBoard();
    }

	private void Start ()
	{
	    StartBoardGame();
	}

    private void CreateInitialBoard(float offsetX, float offsetY)
    {
        Vector2 startingPos = transform.position;
        int[] previousRow = new int[_yTitles];
        int[] previousPreviousRow = new int[_yTitles];
        
        for (int i = 0; i < _xTitles; i++)
        {
            int underType = -1;
            int underUnderType = -1;
            for (int j = 0; j < _yTitles; j++)
            {
                int[] availableToChoose = new int[_gameSprites.Length];
                Vector2 positionToSpawn = new Vector2(startingPos.x + offsetX * i, startingPos.y + offsetY * j);
                GameObject title = Instantiate(_titleSmallPrefab, positionToSpawn,Quaternion.identity);
                holdingMasterObjects.Add(title);
                title.transform.parent = transform;
                if (i > 1)
                {
                    if(previousRow[j] == previousPreviousRow[j])
                        availableToChoose[previousRow[j]] = -1;
                }
                if (j > 1)
                {
                    if (underType == underUnderType)
                        availableToChoose[underType] = -1;
                }
                int spriteToChoose = Random.Range(0, _gameSprites.Length);
                while (availableToChoose[spriteToChoose] == -1)
                {
                    spriteToChoose = Random.Range(0, _gameSprites.Length);
                }
                masterBoard[i, j] = spriteToChoose;
                title.GetComponent<SpriteRenderer>().sprite = _gameSprites[spriteToChoose];
                numOfEachSprite[spriteToChoose]++;
                underUnderType = underType;
                underType = spriteToChoose;
                previousPreviousRow[j] = previousRow[j];
                previousRow[j] = spriteToChoose;
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
                    spriteToChoose = Random.Range(0,numOfEachSprite.Length);
                }
                
                title.GetComponent<SpriteRenderer>().sprite = _gameSprites[spriteToChoose];
                Tile temptile = title.GetComponent<Tile>();
                temptile.SetCoordinate(i,j);
                temptile.SetType(spriteToChoose);
                numOfEachSprite[spriteToChoose]--;
            }
        }
    }

   

    private  void ClearGameBoard()
    {
        for (int i = 0; i < _xTitles; i++)
        {
            for (int j = 0; j < _yTitles; j++)
            {
                Destroy(gameBoard[i,j]);
                gameBoard[i, j] = null;
            }
        }
    }

    private void ClearMasterBoard()
    {
        for (int i = 0; i < _xTitles; i++)
        {
            for (int j = 0; j < _yTitles; j++)
            {
                masterBoard[i, j] = -1;
                GameObject temp = holdingMasterObjects[0];
                holdingMasterObjects.RemoveAt(0);
                Destroy(temp);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ClearGame();
            StartBoardGame();
        }
    }
}
