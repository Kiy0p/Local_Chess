using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class PiecesGenerator : MonoBehaviour
{
    public GameObject piecePrefab;
    public GameObject container;

    private List<GameObject> pieces = new List<GameObject>();

    private void createPieces(PieceData type)
    {
        GameObject piece = Instantiate(piecePrefab, container.transform);
        PieceInfo script = piece.GetComponent<PieceInfo>();
        script.SetValues(type);
        pieces.Add(piece);
    }

    
    public List<GameObject> GeneratePieces(Set set)
    {
        createPieces(set.wRook);
        createPieces(set.wKnight);
        createPieces(set.wBishop);
        createPieces(set.wQueen);
        createPieces(set.wKing);
        createPieces(set.wBishop);
        createPieces(set.wKnight);
        createPieces(set.wRook);
        for (int i = 0; i < 8; i++)
            createPieces(set.wPond);

        for (int i = 0; i < 8; i++)
            createPieces(set.bPond);
        createPieces(set.bRook);
        createPieces(set.bKnight);
        createPieces(set.bBishop);
        createPieces(set.bQueen);
        createPieces(set.bKing);
        createPieces(set.bBishop);
        createPieces(set.bKnight);
        createPieces(set.bRook);

        return (pieces);
    }
}
