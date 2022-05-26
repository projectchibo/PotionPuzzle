using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBehaviour : MonoBehaviour
{
    private Vector2Int _boardPosition;
    public Vector2Int BoardPosition
    {
        get
        {
            return _boardPosition;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBoardPosition(Vector2Int newPosition)
    {
        _boardPosition = newPosition;
    }

    public void GoBackToPosition()
    {

    }
}
