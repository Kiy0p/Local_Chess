using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public enum turn
    {
        White,
        Black
    };

    public float whiteTime;
    public float blackTime;

    public float whiteIncrement;
    public float blackIncrement;

    public string whiteString;
    public string blackString;

    public bool matchStart;
    public turn currentTurn;

    private void Start()
    {
        whiteTime = 10;
        blackTime = 10;
        whiteIncrement = 2;
        blackIncrement = 2;

        whiteString = "";
        blackString = "";
        matchStart = false;
        currentTurn = turn.White;

    }

    private void Update()
    {
        if (matchStart == true)
        {
            if (whiteTime > 0 && currentTurn == turn.White)
            {
                whiteTime -= Time.deltaTime;
                float minutes = Mathf.FloorToInt(whiteTime / 60);
                float seconds = Mathf.FloorToInt(whiteTime % 60);
                seconds += whiteIncrement; 
                whiteString = string.Concat(minutes.ToString(), ":", seconds.ToString());
            }
            else if (blackTime > 0 && currentTurn == turn.Black)
            {
                blackTime -= Time.deltaTime;
                float minutes = Mathf.FloorToInt(blackTime / 60);
                float seconds = Mathf.FloorToInt(blackTime % 60);
                seconds += blackIncrement;
                blackString = string.Concat(minutes.ToString(), ":", seconds.ToString());
            }
        }
    }

    public void switchTurn()
    {
        if (currentTurn == turn.White)
            currentTurn = turn.Black;
        else
            currentTurn = turn.White;
    }

    public void addIncrement()
    {
        if (currentTurn == turn.White)
            whiteTime += whiteIncrement;
        else
            blackTime += blackIncrement;
    }

    public void startMatch()
    {
        if (matchStart == false)
            matchStart = true;
    }
}
