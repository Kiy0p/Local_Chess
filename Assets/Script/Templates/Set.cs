using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEditor;

[CreateAssetMenu(menuName = "Chess/Set")]
public class Set : ScriptableObject
{
    [Header("Black")]
    public PieceData bPond;
    public PieceData bKnight;
    public PieceData bBishop;
    public PieceData bRook;
    public PieceData bQueen;
    public PieceData bKing;

    [Header("White")]
    public PieceData wPond;
    public PieceData wKnight;
    public PieceData wBishop;
    public PieceData wRook;
    public PieceData wQueen;
    public PieceData wKing;

    [Header("Board")]
    public TileBase bSquare;
    public TileBase wSquare;
}
