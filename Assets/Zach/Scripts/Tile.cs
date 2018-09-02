using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DG.Tweening;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public static bool tileAlreadySelected = false;
    public static GameObject previousTileSelected;
    [SerializeField]
    private int type;

    [SerializeField] private AudioClip selectClip;
    [SerializeField] private AudioClip swapClip;
    private AudioSource audioSource;

    private int xCoord;
    private int yCoord;

    public bool isSelected = false;

	// Update is called once per frame

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
	

    public void SetType(int type)
    {
        this.type = type;
    }

    public int GetNumberType()
    {
        return this.type;
    }

    public void SetCoordinate(int x, int y)
    {
        xCoord = x;
        yCoord = y;
    }

    public Vector2 GetCoordinate()
    {
        return new Vector2(xCoord,yCoord);
    }

    private void OnMouseDown()
    {
        StartCoroutine(OnMouseDownCoroutine());
    }
    private IEnumerator OnMouseDownCoroutine()
    {
        if (isSelected)
        {
            previousTileSelected = null;
            tileAlreadySelected = false;
            isSelected = false;
            transform.localScale -= new Vector3(.2f,.2f,0);
        }
        else
        { 
            if (tileAlreadySelected && previousTileSelected != gameObject)
            {
                if(CheckForValidSwap(gameObject,previousTileSelected))
                {
                    audioSource.PlayOneShot(swapClip,4.0f);
                    Swap(gameObject,previousTileSelected);
                    yield return new WaitForSeconds(.3f);
                    BoardManager manager = GameObject.FindWithTag("Board Manager").GetComponent<BoardManager>();
                    barFill tempBarFill = manager.gameObject.GetComponent<barFill>();
                    tempBarFill.updateBar(manager.CheckForThrees());
                    GameObject.FindWithTag("Board Manager").GetComponent<BoardManager>().SpawnAngries();
                    if (manager.CheckForWin())
                    {
                        manager.Win();
                    }
                    tileAlreadySelected = false;
                    previousTileSelected.transform.localScale -= new Vector3(.2f, .2f, 0);
                    Tile tempTile = previousTileSelected.GetComponent<Tile>();
                    tempTile.isSelected = false;
                    previousTileSelected = null;
                }
                else
                {
                    Tile otherSelected = previousTileSelected.GetComponent<Tile>();
                    previousTileSelected.transform.localScale -= new Vector3(.2f, .2f, 0);
                    otherSelected.isSelected = false;
                    previousTileSelected = gameObject;
                    transform.localScale += new Vector3(.2f, .2f, 0);
                    isSelected = true;
                }
            }
            else
            {
                audioSource.PlayOneShot(selectClip,0.25f);
                tileAlreadySelected = true;
                previousTileSelected = gameObject;
                isSelected = true;
                transform.localScale += new Vector3(.2f, .2f, 0);
            }
           
        }
    }

    private bool CheckForValidSwap(GameObject go1, GameObject go2)
    {
      
        Tile tile1 = go1.GetComponent<Tile>();
        Tile tile2 = go2.GetComponent<Tile>();
        Vector2 tile1Coordinate = tile1.GetCoordinate();
        Vector2 tile2Coordinate = tile2.GetCoordinate();
        int x1 = (int)tile1Coordinate.x;
        int y1 = (int)tile1Coordinate.y;
        int x2 = (int)tile2Coordinate.x;
        int y2 = (int)tile2Coordinate.y;
        //vertical
        if (x1 == x2)
        {
            GameObject boardmanagerobject = GameObject.FindWithTag("Board Manager");
            BoardManager boardManager = boardmanagerobject.GetComponent<BoardManager>();
            GameObject[,] board = boardManager.getBoard();
            bool[] hasType = new bool[4];
            for (int i = Mathf.Min(y1, y2); i <= Mathf.Max(y1, y2); i++)
            {
                Tile tempTile = board[x1, i].GetComponent<Tile>();
                int tempType = tempTile.GetNumberType();
                if (hasType[tempType])
                {
                    return false;
                }
                else
                {
                    hasType[tempType] = true;
                }
            }
            return true;
        }
        //horizontal
        else if (y1 == y2)
        {
            GameObject boardmanagerobject = GameObject.FindWithTag("Board Manager");
            BoardManager boardManager = boardmanagerobject.GetComponent<BoardManager>();
            GameObject[,] board = boardManager.getBoard();
            bool[] hasType = new bool[4];
            for (int i = Mathf.Min(x1, x2); i <= Mathf.Max(x1, x2); i++)
            {
                Tile tempTile = board[i, y1].GetComponent<Tile>();
                int tempType = tempTile.GetNumberType();
                if (hasType[tempType])
                {
                    return false;
                }
                else
                {
                    hasType[tempType] = true;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    private void Swap(GameObject go1, GameObject go2)
    {
        Tile tempTile1 = go1.GetComponent<Tile>();
        Tile tempTile2 = go2.GetComponent<Tile>();
        GameObject boardmanagerobject = GameObject.FindWithTag("Board Manager");
        BoardManager boardManager = boardmanagerobject.GetComponent<BoardManager>();
        GameObject[,] board = boardManager.getBoard();
        Vector2 tempCoordinate1 = tempTile1.GetCoordinate();
        Vector2 tempCoordinate2 = tempTile2.GetCoordinate();
        int x1 = (int)tempCoordinate1.x;
        int y1 = (int)tempCoordinate1.y;
        int x2 = (int)tempCoordinate2.x;
        int y2 = (int) tempCoordinate2.y;
        if (x1 == x2)
        {
            int diff = Mathf.Abs(y1 - y2);
            switch (diff)
            {
                case 1:
                    GameObject tempObj = go1;
                    board[x1, y1] = go2;
                    board[x2, y2] = tempObj;
                    tempTile1.SetCoordinate(x2,y2);
                    tempTile2.SetCoordinate(x1,y1);
                    Vector3 origPosition1 = go1.transform.position;
                    go1.transform.DOMove(go2.transform.position,.3f);
                    go2.transform.DOMove(origPosition1, .3f);
                    break;
                case 2:
                    tempObj = go1;
                    board[x1, y1] = go2;
                    board[x2, y2] = tempObj;
                    tempTile1.SetCoordinate(x2, y2);
                    tempTile2.SetCoordinate(x1, y1);
                    origPosition1 = go1.transform.position;
                    go1.transform.DOMove(go2.transform.position, .3f);
                    go2.transform.DOMove(origPosition1, .3f);
                    break;
                case 3:
                    //swap outside 2
                    tempObj = go1;
                    board[x1, y1] = go2;
                    board[x2, y2] = tempObj;
                    tempTile1.SetCoordinate(x2, y2);
                    tempTile2.SetCoordinate(x1, y1);
                    origPosition1 = go1.transform.position;
                    go1.transform.DOMove(go2.transform.position, .3f);
                    go2.transform.DOMove(origPosition1, .3f);
                    //swapInside 2
                    int min = Mathf.Min(y1, y2);
                    tempObj = board[x1, min + 1];
                    board[x1, min + 1] = board[x1, min + 2];
                    board[x1, min + 2] = tempObj;
                    tempTile1 = board[x1, min + 2].GetComponent<Tile>();
                    tempTile2 = board[x1, min + 1].GetComponent<Tile>();
                    tempTile1.SetCoordinate(x1,min + 2);
                    tempTile2.SetCoordinate(x1, min + 1);
                    origPosition1 = board[x1, min + 2].transform.position;
                    board[x1, min + 2].transform.DOMove(board[x1, min + 1].transform.position, .3f);
                    board[x1, min + 1].transform.DOMove(origPosition1, .3f);
                    break;
                default:
                    return;
            }
        }
        else
        {
            int diff = Mathf.Abs(x1 - x2);
            switch (diff)
            {
                case 1:
                    GameObject tempObj = go1;
                    board[x1, y1] = go2;
                    board[x2, y2] = tempObj;
                    tempTile1.SetCoordinate(x2, y2);
                    tempTile2.SetCoordinate(x1, y1);
                    Vector3 origPosition1 = go1.transform.position;
                    go1.transform.DOMove(go2.transform.position, .3f);
                    go2.transform.DOMove(origPosition1, .3f);
                    break;
                case 2:
                    tempObj = go1;
                    board[x1, y1] = go2;
                    board[x2, y2] = tempObj;
                    tempTile1.SetCoordinate(x2, y2);
                    tempTile2.SetCoordinate(x1, y1);
                    origPosition1 = go1.transform.position;
                    go1.transform.DOMove(go2.transform.position, .3f);
                    go2.transform.DOMove(origPosition1, .3f);
                    break;
                case 3:
                    //move outside 2
                    tempObj = go1;
                    board[x1, y1] = go2;
                    board[x2, y2] = tempObj;
                    tempTile1.SetCoordinate(x2, y2);
                    tempTile2.SetCoordinate(x1, y1);
                    origPosition1 = go1.transform.position;
                    go1.transform.DOMove(go2.transform.position, .3f);
                    go2.transform.DOMove(origPosition1, .3f);
                    //move middle 2
                    int min = Mathf.Min(x1, x2);
                    tempObj = board[min+1, y1];
                    board[min+1, y1] = board[min + 2, y1];
                    board[min + 2, y1] = tempObj;
                    tempTile1 = board[min + 2, y1].GetComponent<Tile>();
                    tempTile2 = board[min+1, y1].GetComponent<Tile>();
                    tempTile1.SetCoordinate(min + 2, y1);
                    tempTile2.SetCoordinate(min+1, y1);
                    origPosition1 = board[min + 2, y1].transform.position;
                    board[min + 2, y1].transform.DOMove(board[min+1, y1].transform.position, .3f);
                    board[min+1, y1].transform.DOMove(origPosition1, .3f);
                    break;
                default:
                    return;
            }
        }
    }
}
