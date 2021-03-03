using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RotationManager : MonoBehaviour
{
    private Camera mainCam;
    private Animator pieceAnim;

    private void Start()
    {
        mainCam = Camera.main;
    }

    public void rotateAll(List<GameObject> pieces)
    {
        RotateMap();
        RotatePieces(pieces);
    }

    private void RotateMap()
    {
        mainCam.GetComponent<Animator>().SetTrigger("camRota");
    }

    private void RotatePieces(List<GameObject> pieces)
    {
        for (int i = 0; i < pieces.Count; i++) {
            if (pieces[i].GetComponent<PieceInfo>().isActive == true) {
                pieceAnim = pieces[i].GetComponent<Animator>();
                pieceAnim.SetTrigger("pieceRota");
            }
        }
    }
}
