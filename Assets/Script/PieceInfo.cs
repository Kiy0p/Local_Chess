using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceInfo : MonoBehaviour
{
    public string color;
    public string type;
    public bool isActive;
    public bool moved;
    public int value;

    private SpriteRenderer render;
     
    public void SetValues(PieceData data)
    {
        render = this.GetComponent<SpriteRenderer>();
        color = data.color;
        type = data.type;
        isActive = data.isActive;
        moved = data.moved;
        value = data.value;
        render.sprite = data.skin;
    }
}
