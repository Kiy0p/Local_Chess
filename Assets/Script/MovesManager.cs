using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesManager : MonoBehaviour
{
    public bool ChooseMove(GameObject[,] board, Vector3Int prevPos, Vector3Int newPos, GameObject piece, bool takes)
    {
        string type = piece.GetComponent<PieceInfo>().type;
        GetCollisions(prevPos, newPos);
        switch (type)
        {
            case "queen":
                return (QueenMove(prevPos, newPos, takes));
            case "king":
                return (KingMove(board, prevPos, newPos, piece, takes));
            case "rook":
                return (RookMove(prevPos, newPos, takes));
            case "bishop":
                return (BishopMove(prevPos, newPos, takes));
            case "knight":
                return (KnightMove(prevPos, newPos));
            case "pond":
                return(PondMove(prevPos, newPos, piece, takes));
            default:
                return (false);
        }
    }

    // Pieces moves Rules

    private bool KingMove(GameObject[,] board, Vector3Int prevPos, Vector3Int newPos, GameObject piece, bool takes)
    {
        if (GetCollisions(prevPos, newPos) == 1 && takes == false)
            return (false);
        if (IsVertical(prevPos, newPos) == 1 || IsHorizontal(prevPos, newPos) == 1)
        {
            piece.GetComponent<PieceInfo>().moved = true;
            return (true);
        }
        if (IsDiagonalOne(prevPos, newPos) == 1 || IsDiagonalTwo(prevPos, newPos) == 1)
        {
            piece.GetComponent<PieceInfo>().moved = true;
            return (true);
        }
        if (Castle(board, prevPos, newPos, piece) == true)
            return (true);
        return (false);
    }
    private bool QueenMove(Vector3Int prevPos, Vector3Int newPos, bool takes)
    {
        if (GetCollisions(prevPos, newPos) == 1 && takes == false)
            return (false);
        if (IsVertical(prevPos, newPos) != 0 || IsHorizontal(prevPos, newPos) != 0)
            return (true);
        if (IsDiagonalOne(prevPos, newPos) != 0 || IsDiagonalTwo(prevPos, newPos) != 0)
            return (true);
        return (false);
    }

    private bool RookMove(Vector3Int prevPos, Vector3Int newPos, bool takes)
    {
        if (GetCollisions(prevPos, newPos) == 1 && takes == false)
            return (false);
        if (IsVertical(prevPos, newPos) != 0 || IsHorizontal(prevPos, newPos) != 0)
            return (true);
        return (false);
    }

    private bool BishopMove(Vector3Int prevPos, Vector3Int newPos, bool takes)
    {
        if (GetCollisions(prevPos, newPos) == 1 && takes == false)
            return (false);
        if (IsDiagonalOne(prevPos, newPos) != 0 || IsDiagonalTwo(prevPos, newPos) != 0)
            return (true);
        return (false);
    }

    private bool KnightMove(Vector3Int prevPos, Vector3Int newPos)
    {
        if (newPos == prevPos + new Vector3Int(1, 2, 0) || newPos == prevPos + new Vector3Int(-1, 2, 0))
            return (true);
        if (newPos == prevPos + new Vector3Int(2, 1, 0) || newPos == prevPos + new Vector3Int(2, -1, 0))
            return (true);
        if (newPos == prevPos + new Vector3Int(1, -2, 0) || newPos == prevPos + new Vector3Int(-1, -2, 0))
            return (true);
        if (newPos == prevPos + new Vector3Int(-2, -1, 0) || newPos == prevPos + new Vector3Int(-2, 1, 0))
            return (true);
        return (false);
    }

    private bool PondMove(Vector3Int prevPos, Vector3Int newPos, GameObject piece, bool takes)
    {
        if (piece.GetComponent<PieceInfo>().moved == false)
        {
            if (IsVertical(prevPos, newPos) == 1 && takes == false)
            {
                piece.GetComponent<PieceInfo>().moved = true;
                return (true);
            }
            if (IsVertical(prevPos, newPos) == 2 && takes == false)
            {
                piece.GetComponent<PieceInfo>().moved = true;
                return (true);
            }
        }
        if (IsVertical(prevPos, newPos) == 1 && takes == false)
        {
            return (true);
        }
        else if ((IsDiagonalOne(prevPos, newPos) == 1 || IsDiagonalTwo(prevPos, newPos) == 1) && takes == true)
        {
            piece.GetComponent<PieceInfo>().moved = true;
            return (true);
        }
        return (false);
    }


    // Special Movements

    private bool Castle(GameObject[,] board, Vector3Int prevPos, Vector3Int newPos, GameObject piece)
    {
        if (newPos.x == 6 && piece.GetComponent<PieceInfo>().moved == false && board[newPos.y, 7].GetComponent<PieceInfo>().moved == false)
        {
            Vector3Int rookPosition = new Vector3Int(5, newPos.y, 0);

            board[newPos.y, 5] = board[newPos.y, 7];
            board[newPos.y, 7] = null;
            board[newPos.y, 5].transform.position = RealCoordinates(rookPosition);
            board[newPos.y, 5].GetComponent<PieceInfo>().moved = true;
            return (true);
        }
        if (newPos.x == 2 && piece.GetComponent<PieceInfo>().moved == false && board[newPos.y, 0].GetComponent<PieceInfo>().moved == false)
        {
            if (GetCollisions(new Vector3Int(0, newPos.y, 0), new Vector3Int(prevPos.x, prevPos.y, 0)) <= 1)
            {
                Vector3Int rookPosition = new Vector3Int(3, newPos.y, 0);

                board[newPos.y, 3] = board[newPos.y, 0];
                board[newPos.y, 0] = null;
                board[newPos.y, 3].transform.position = RealCoordinates(rookPosition);
                board[newPos.y, 3].GetComponent<PieceInfo>().moved = true;
                piece.GetComponent<PieceInfo>().moved = true;
                return (true);
            }
        }
        return (false);
    }



    // Calculation Mathods

    private Vector3 RealCoordinates(Vector3Int coordinates)
    {
        return (new Vector3(coordinates.x + 0.5f, coordinates.y + 0.5f, coordinates.z));
    }

    private int GetCollisions(Vector3Int prevPos, Vector3Int newPos)
    {
        Vector3 realPrevPos = RealCoordinates(prevPos);
        Vector3 realNewPos = RealCoordinates(newPos);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(realPrevPos, realNewPos - realPrevPos, Vector3.Distance(realPrevPos, realNewPos));
        return (hits.Length);
    }


    private int IsVertical(Vector3Int prevPos, Vector3Int newPos)
    {
        if (prevPos.x == newPos.x)
            return (Mathf.Abs(prevPos.y - newPos.y));
        return (0);
    }

    private int IsHorizontal(Vector3Int prevPos, Vector3Int newPos)
    {
        if (prevPos.y == newPos.y)
            return (Mathf.Abs(prevPos.x - newPos.x));
        return (0);
    }

    private int IsDiagonalOne(Vector3Int prevPos, Vector3Int newPos)
    {
        Vector3Int vectPos = new Vector3Int(+1, +1, 0);
        Vector3Int vectNeg = new Vector3Int(-1, -1, 0);

        for (int i = 1; i < 9; i++)
        {
            if (prevPos + vectPos == newPos)
                return (i);
            if (prevPos + vectNeg == newPos)
                return (i);
            vectNeg.x = vectNeg.x - 1;
            vectNeg.y = vectNeg.y - 1;
            vectPos.x = vectPos.x + 1;
            vectPos.y = vectPos.y + 1;
        }
        return (0);
    }

    private int IsDiagonalTwo(Vector3Int prevPos, Vector3Int newPos)
    {
        Vector3Int vectPos = new Vector3Int(+1, -1, 0);
        Vector3Int vectNeg = new Vector3Int(-1, +1, 0);

        for (int i = 1; i < 9; i++)
        {
            if (prevPos + vectPos == newPos)
                return (i);
            if (prevPos + vectNeg == newPos)
                return (i);
            vectNeg.x = vectNeg.x - 1;
            vectNeg.y = vectNeg.y + 1;
            vectPos.x = vectPos.x + 1;
            vectPos.y = vectPos.y - 1;
        }
        return (0);
    }
}
