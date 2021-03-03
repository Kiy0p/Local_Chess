using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Chess/Piece")]
public class PieceData : ScriptableObject
{
    public string color;
    public string type;
    public bool isActive;
    public int value;
    public Sprite skin;
}
