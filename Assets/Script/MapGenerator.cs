using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Set tileSkins;

    private Tilemap board;
    private int boardSize;

    private void Start()
    {
        boardSize = 8;
        board = this.GetComponent<Tilemap>();
        GenerateBoard(0, 0, 0);
    }
    
    private void GenerateBoard(int xOrigin, int yOrigin, int zOrigin)
    {
        TileBase tile = tileSkins.bSquare;
        Vector3Int position = new Vector3Int(xOrigin, yOrigin, zOrigin);

        for (int i = 0; i < boardSize; i++) {
            for (int j = 0; j < boardSize; j++) {
                
                board.SetTile(position, tile);

                if (tile == tileSkins.bSquare)
                    tile = tileSkins.wSquare;
                else
                    tile = tileSkins.bSquare;
                position.Set(position.x + 1, position.y, position.z);
            }
            if (tile == tileSkins.bSquare)
                tile = tileSkins.wSquare;
            else
                tile = tileSkins.bSquare;
            position.Set(xOrigin, position.y + 1, position.z);
        }
    }
}
