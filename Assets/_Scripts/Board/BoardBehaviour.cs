using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehaviour : MonoBehaviour
{
    [Header("Board Info")]
    public int xSize;
    public int ySize;

    [Header("Pieces Info")]
    public GameObject piecePrefab;
    public float pieceWidth;
    public float pieceHeight;

    public List<PieceBehaviour> allPieces;

    #region Monobehaviour
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //SpawnPiece(new Vector2Int(0, 0));
            SetupBoard();
        }
    }
    #endregion

    #region Board Management
    private void SetupBoard()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                SpawnPiece(new Vector2Int(x, y));
            }
        }
    }

    private void SpawnPiece(Vector2Int boardPosition)
    {
        // Create the piece
        var newPieceGO = Instantiate(piecePrefab, transform);
        var newPieceBehaviour = newPieceGO.GetComponent<PieceBehaviour>();
        if (newPieceBehaviour == null)
        {
            Destroy(newPieceGO);
            Debug.LogError("Your piece prefab doesn't have a piece behaviour.");
            return;
        }

        // Set up the piece
        newPieceBehaviour.SetBoardPosition(boardPosition);
        newPieceGO.transform.localScale = new Vector2(pieceWidth, pieceHeight);
        newPieceGO.transform.localPosition = BoardToWorldPosition(boardPosition);

        // Store reference of the piece
        if (allPieces == null)
            allPieces = new List<PieceBehaviour>();

        allPieces.Add(newPieceBehaviour);
        return;
    }
    #endregion

    #region Utils
    public Vector2 BoardToWorldPosition(Vector2Int boardPosition)
    {
        return new Vector2(boardPosition.x * pieceWidth, - (boardPosition.y * pieceHeight));
    }
    #endregion
}