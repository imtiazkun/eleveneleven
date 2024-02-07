using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using zeltatech;

public class GameManager : MonoBehaviour
{

    public int score;

    [SerializeField]
    TextMeshPro ScoreUI;

    [SerializeField]
    Transform ScoreUIGroup;

    public List<Piece> Pieces;

    public List<Piece> PiecesInQueue;

    [SerializeField]
    Transform drawer; // Contains reference to the parent of piece spawning position


    public static GameManager instance;

    public Color[] blockColors;

    private void Awake()
    {

        instance = this;

        GridManager.OnGridComplete += PopulateDrawer;
    }

    private void OnDestroy()
    {
        GridManager.OnGridComplete -= PopulateDrawer;
    }

    // Start is called before the first frame update
    void Start()
    {
        ScoreUI.SetText(score.ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Color GetRandomColor ()
    {
        int randomColor = UnityEngine.Random.Range(0, blockColors.Length);
        return blockColors[randomColor];
    }


    public void Check(int id)
    {
        Piece piece = PiecesInQueue.Find(item => item.id == id);
        Destroy(piece.gameObject);
        PiecesInQueue.Remove(piece);

        if (PiecesInQueue.Count < 1)
        {
            PopulateDrawer();
        }
    }



    [Button]
    public void PopulateDrawer()
    {

        foreach (Transform _child in drawer)
        {
            Piece _randomPiece = GetRandomPiece();

            GameObject _randomPieceInstantiated = Instantiate(_randomPiece.gameObject, _child.position, Quaternion.identity);
            _randomPieceInstantiated.GetComponent<Piece>().id = PiecesInQueue.Count;
            _randomPieceInstantiated.gameObject.name = PiecesInQueue.Count.ToString();
            PiecesInQueue.Add(_randomPieceInstantiated.GetComponent<Piece>());
        }
    }


    Piece GetRandomPiece()
    {
        int _randomIndex = UnityEngine.Random.Range(0, Pieces.Count);
        return Pieces[_randomIndex];
    }

    [Button]
    public void PrintQueueDetails()
    {
        Debug.Log("Queue Count: " + PiecesInQueue.Count);
        for (int i = 0; i < PiecesInQueue.Count; i++)
        {
            Debug.Log("Piece ID: " + PiecesInQueue[i].id);
        }
    }


    void RefreshGrid()
    {
        Debug.Log("Refreshing Grid");

        for (int row = 0; row < GridManager.instance.grid.GetLength(0); row++)
        {
            for (int col = 0; col < GridManager.instance.grid.GetLength(1); col++)
            {
                if (GridManager.instance.grid[col, row].isFilled == false)
                {
                    GridManager.instance.grid[col, row].SetColor(GridManager.instance.grid[col, row].blockColor);
                }
            }
        }
    }



    public bool isValidPosition(Block[] _childBlocks)
    {
        int count = 0;

        foreach (Block item in _childBlocks)
        {
            if (item.blockPos.x >= 0 && item.blockPos.x <= 10 && item.blockPos.y >= 0 && item.blockPos.y <= 10)
            {
                if (GridManager.instance.grid[(int)item.blockPos.x, (int)item.blockPos.y].isFilled == false)
                {
                    count++;
                }
            }
            else
            {
                return false;
            }
        }

        if (count == _childBlocks.Length)
        {
            return true;
        }
        else
        {
            return false;
        }

    }




    [Button]
    public void UpdateScore()
    {
        score += 5;

        Debug.Log(ScoreUI.transform.position);


        ScoreUIGroup.DOJump(ScoreUIGroup.position, 0.15f, 1, 0.1f).OnComplete(() =>
        {
            ScoreUI.SetText(score.ToString());
        });
    }

}
