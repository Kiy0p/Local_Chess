using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{
    public Set set; // Set working with

    // Objects for UI, rotation and initialization of the pieces
    public VerticalLayoutGroup whiteLayout;
    public VerticalLayoutGroup blackLayout;
    public Text whiteScore;
    public Text blackScore;
    public RotationManager rotationAnimator;
    public InitGame initGame;

    // Board and pieces
    private GameObject selectedPiece;
    private Vector3Int prevMove;

    private GameObject[,] board;
    private PiecesGenerator script;
    private List<GameObject> pieces;

    void Start()
    {
        board = new GameObject[8, 8];
        selectedPiece = null;
        script = this.GetComponent<PiecesGenerator>();
        pieces = script.GeneratePieces(set);
        initGame.InitPieces(board, pieces);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int coordinates = Vector3Int.FloorToInt(worldPoint);
            PieceMovement(coordinates);
        }
    }

    private void PieceMovement(Vector3Int coordinates)
    {
        if (selectedPiece != null) {
            MakeMovement(coordinates);
            return;
        }

        if (board[coordinates.y, coordinates.x] != null && board[coordinates.y, coordinates.x].GetComponent<PieceInfo>().isActive == true) {
            selectedPiece = board[coordinates.y, coordinates.x];
            prevMove = coordinates;
        } else {
            selectedPiece = null;
        }
    }

    private Vector3 RealCoordinates(Vector3Int coordinates)
    {
        return (new Vector3(coordinates.x + 0.5f, coordinates.y + 0.5f, coordinates.z));
    }

    private void MakeMovement(Vector3Int coordinates)
    {
        GameObject piece = board[coordinates.y, coordinates.x];

        if (piece == null) { // Makes a movement
            board[coordinates.y, coordinates.x] = selectedPiece;
            board[prevMove.y, prevMove.x] = null;
            selectedPiece.transform.position = RealCoordinates(coordinates);
            selectedPiece = null;
            rotationAnimator.rotateAll(pieces);
        } else {
            if (piece.GetComponent<PieceInfo>().color == selectedPiece.GetComponent<PieceInfo>().color) {
                selectedPiece = board[coordinates.y, coordinates.x];
                prevMove = coordinates;
            } else { // Makes a movement
                StorePiece(piece);
                board[coordinates.y, coordinates.x] = selectedPiece;
                board[prevMove.y, prevMove.x] = null;
                selectedPiece.transform.position = RealCoordinates(coordinates);
                selectedPiece = null;
                rotationAnimator.rotateAll(pieces);
            }
        }
    }

    private void StorePiece(GameObject piece)
    {
        piece.GetComponent<PieceInfo>().isActive = false;
        piece.transform.localScale = new Vector3(50, 50, 0);
        piece.transform.rotation = new Quaternion(0, 0, 0, 0);
        piece.AddComponent<RectTransform>();
        piece.AddComponent<Image>().sprite = piece.GetComponent<SpriteRenderer>().sprite;
        Destroy(piece.GetComponent<SpriteRenderer>());
        Destroy(piece.GetComponent<Animator>());

        if (piece.GetComponent<PieceInfo>().color == "white")
        {
            piece.transform.SetParent(blackLayout.transform, false);
            blackScore.text =  (int.Parse(blackScore.text) + piece.GetComponent<PieceInfo>().value).ToString();
        }
        else
        {
            piece.transform.SetParent(whiteLayout.transform, false);
            whiteScore.text = (int.Parse(whiteScore.text) + piece.GetComponent<PieceInfo>().value).ToString();
        }
    }
}
