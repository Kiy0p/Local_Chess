using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGame : MonoBehaviour
{
    public void InitPieces(GameObject[,] board, List<GameObject> pieces)
    {
        FillBoard(board, pieces);
        placeBoard(board);
    }

    private void FillBoard(GameObject[,] board, List<GameObject> pieces)
    {
        int index = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (i < 2 || i >= 6)
                {
                    board[i, j] = pieces[index];
                    index++;
                }
                else
                {
                    board[i, j] = null;
                }
            }
        }
    }
    private void placeBoard(GameObject[,] board)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j] != null)
                    board[i, j].transform.position = new Vector3(0.5f + j, 0.5f + i, 0);
            }
        }
    }
}
