using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public PieceBehaviour curSelectedPiece;
    public BoardBehaviour board;
    public float checkRadius;

    public InputManager.MouseButton mouseButton;

    // Start is called before the first frame update
    void Start()
    {
        mouseButton = InputManager.Instance.GetMouseButtonById("LeftMouse");
        mouseButton.OnButtonPressedEvent += CheckPieceInMousePos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckPieceInMousePos()
    {
        CheckPieceInPosition(mouseButton.MouseWorldPos);
    }

    public void CheckPieceInPosition(Vector3 position)
    {
        int mask = LayerMask.GetMask("Pieces");
        var hit = Physics2D.CircleCast(position, checkRadius, Vector2.up, 0, mask);
        if (hit)
        {
            var newPiece = hit.collider.GetComponent<PieceBehaviour>();
            if (newPiece == null) return;
            curSelectedPiece = newPiece;
        }
    }

    public void SelectPiece(PieceBehaviour newPiece)
    {
        if (curSelectedPiece != null) curSelectedPiece.GoBackToPosition();
    }
}
