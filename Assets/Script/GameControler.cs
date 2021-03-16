using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{
    [Header("Set")]
    public Set set; // Set working with

    [Header("UI")]
    // Objects for UI, rotation and initialization of the pieces
    public VerticalLayoutGroup whiteLayout;
    public VerticalLayoutGroup blackLayout;
    public Text whiteScore;
    public Text blackScore;
    public Text whiteTimer;
    public Text blackTimer;

    [Header("Scripts")]
    // Scripts
    public RotationManager rotationAnimator;
    public MovesManager movesManager;
    public InitGame initGame;
    public Timer time;

    // Board and pieces
    private GameObject selectedPiece;
    private Vector3Int prevPos;

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
        whiteTimer.text = time.whiteString;
        blackTimer.text = time.blackString;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int newPos = Vector3Int.FloorToInt(worldPoint);
            PieceMovement(newPos);
        }
    }

    private void PieceMovement(Vector3Int newPos)
    {
        if (newPos.x > 7  || newPos.x < 0 || newPos.y > 7 || newPos.y < 0)
            return;

        if (selectedPiece != null) {
            MakeMovement(newPos);
            return;
        } else if (board[newPos.y, newPos.x] != null && board[newPos.y, newPos.x].GetComponent<PieceInfo>().isActive == true) {
            selectedPiece = board[newPos.y, newPos.x];
            prevPos = newPos;
        } else {
            selectedPiece = null;
        }
    }

    private Vector3 RealCoordinates(Vector3Int newPos)
    {
        return (new Vector3(newPos.x + 0.5f, newPos.y + 0.5f, newPos.z));
    }

    private void MakeMovement(Vector3Int newPos)
    {
        GameObject piece = board[newPos.y, newPos.x];

        if (piece != null && piece.GetComponent<PieceInfo>().color == selectedPiece.GetComponent<PieceInfo>().color) {
            selectedPiece = board[newPos.y, newPos.x];
            prevPos = newPos;
        }
        if (piece == null && movesManager.ChooseMove(board, prevPos, newPos, selectedPiece, false)) { // Makes a movement
            board[newPos.y, newPos.x] = selectedPiece;
            board[prevPos.y, prevPos.x] = null;
            selectedPiece.transform.position = RealCoordinates(newPos);
            selectedPiece = null;
            rotationAnimator.rotateAll(pieces);
            time.switchTurn();
            time.addIncrement();
            time.startMatch();
        }
        else if (piece != null && movesManager.ChooseMove(board, prevPos, newPos, selectedPiece, true)) { // Makes a movement
            StorePiece(piece);
            board[newPos.y, newPos.x] = selectedPiece;
            board[prevPos.y, prevPos.x] = null;
            selectedPiece.transform.position = RealCoordinates(newPos);
            selectedPiece = null;
            rotationAnimator.rotateAll(pieces);
            time.switchTurn();
            time.addIncrement();
        }
        else {
            selectedPiece = null;
            prevPos = newPos;
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
